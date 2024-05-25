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
        public async Task<IActionResult> UploadFile(string userId, IFormFile file, string description)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is null or empty.");
            }

            try
            {
                using var stream = file.OpenReadStream();

                var fileCreationDto = new FileCreationDto
                {
                    FileName = file.FileName,
                    Description = description, // Assuming Description is optional and not passed here
                    ContentType = file.ContentType,
                    Content = stream // Assuming FileStream is the property for the file content stream in FileCreationDto
                };

                var uploadedFile = await _fileService.UploadFileAsync(userId, fileCreationDto);
                return Ok(new { FileId = uploadedFile._id, FileName = uploadedFile.FileName });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                return BadRequest("File ID is null or empty.");
            }
            try
            {
                var file = await _fileService.DownloadFileAsync(fileId);
                if (file == null || file.Content == null)
                {
                    return NotFound("File not found.");
                }

                return File(file.Content, file.ContentType, file.FileName, file.CreatedAt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to download file: {fileId}");
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading file: {ex.Message}");
            }
        }
        //{
        //    

        //    try
        //    {
        //        var fileDto = await _fileService.DownloadFileAsync(id);
        //        if (fileDto == null || fileDto.Content == null)
        //        {
        //            return NotFound("File not found.");
        //        }

        //        // You can include any metadata properties you need in the FileResultDto
        //        var resultDto = new FileResultDto
        //        {

        //            // Include other metadata properties as needed
        //            Content = fileDto.Content,
        //            ContentType = fileDto.ContentType,
        //            FileName = fileDto.FileName

        //        };

        //        return Ok(resultDto); // Return the DTO object containing both the file content and metadata
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (not implemented here)
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading file: {ex.Message}");
        //    }
        //}





    }
}
