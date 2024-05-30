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
            using var stream = dto.Content.OpenReadStream();

            var uploadedFile = await _fileService.UploadFileAsync(dto);
            return Ok(new { FileId = uploadedFile._id, FileName = uploadedFile.FileName });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("File ID is null or empty.");
            }

            var file = await _fileService.DownloadFileAsync(id);
            if (file == null || file.Content == null)
            {
                return NotFound("File not found.");
            }

            Response.Headers.Add("X-User-ID", file.UserId);
            Response.Headers.Add("X-Description", file.Description);

            return File(file.Content, file.ContentType, file.FileName);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("File ID is null or empty.");
            }

            await _fileService.DeleteFileAsync(id);
            return Ok(new { Message = "File deleted successfully." });
        }

        [HttpGet("user/{userId}/files")]
        public async Task<IActionResult> GetFilesByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is null or empty.");
            }

            var files = await _fileService.GetFilesByUserIdAsync(userId);
            if (files == null || !files.Any())
            {
                return NotFound("No files found for the user.");
            }

            return Ok(files);
        }


    }
}
