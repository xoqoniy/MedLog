using MedLog.Service.DTOs.FileDTOs;
using MedLog.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MedLog.API.Controllers
{

    public class FileController : RestFulSense
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected");
            }

            using var stream = file.OpenReadStream();
            var fileCreationDto = new FileCreationDto
            {
                UserId = userId,
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileStream = stream
            };

            var result = await _fileService.UploadFileAsync(fileCreationDto);
            return Ok(result);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(string id)
        {
            var fileResult = await _fileService.DownloadFileAsync(id);
            if (fileResult == null)
            {
                return NotFound();
            }

            return File(fileResult.FileStream, fileResult.ContentType, fileResult.FileName);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            var result = await _fileService.DeleteFileAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("update")]
        public IActionResult UpdateFile([FromBody] FileUpdateDto fileUpdateDto)
        {
            var result = _fileService.UpdateFile(fileUpdateDto);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
