using Microsoft.AspNetCore.Http;

namespace MedLog.Service.DTOs.FileDTOs;
#pragma warning disable 
public class FileCreationDto
{
    public IFormFile Content { get; set; } // The file content to be uploaded
    public string UserId { get; set; }
    public string Description { get; set; } 
}
