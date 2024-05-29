using MedLog.Service.DTOs.FileDTOs;
using MedLog.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MedLog.API.Controllers
{

    public class FileController : RestFulSense
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm]FileCreationDto dto)
        {
            if (dto == null || dto.Content.Length == 0)
            {
                return BadRequest("File is null or empty.");
            }

            try
            {
                using var stream = dto.Content.OpenReadStream();

                //var fileCreationDto = new FileCreationDto
                //{
                //    FileName = dto.Content.FileName,
                //    Description = dto.Descriptiondescription, // Assuming Description is optional and not passed here
                //    ContentType = file.ContentType,
                //    Content = stream // Assuming FileStream is the property for the file content stream in FileCreationDto
                //};

                var uploadedFile = await _fileService.UploadFileAsync(dto);
                return Ok(new { FileId = uploadedFile._id, FileName = uploadedFile.FileName });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("File ID is null or empty.");
            }

            try
            {
                var file = await _fileService.DownloadFileAsync(id);
                if (file == null || file.Content == null)
                {
                    return NotFound("File not found.");
                }

                Response.Headers.Add("X-User-ID", file.UserId);
                Response.Headers.Add("X-Description", file.Description);

                return File(file.Content, file.ContentType, file.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to download file: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading file: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("File ID is null or empty.");
            }

            try
            {
                await _fileService.DeleteFileAsync(id);
                return Ok(new { Message = "File deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in file deleting");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting file: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}/files")]
        public async Task<IActionResult> GetFilesByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is null or empty.");
            }

            try
            {
                var files = await _fileService.GetFilesByUserIdAsync(userId);
                if (files == null || !files.Any())
                {
                    return NotFound("No files found for the user.");
                }

                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch files for user: {userId}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching files: {ex.Message}");
            }
        }


    }
}
