namespace MedLog.Service.DTOs.FileDTOs;
#pragma warning disable 
public class FileCreationDto
{
    public string FileName { get; set; } // Name of the file
    public string Description { get; set; } 
    public string ContentType { get; set; } // Content type of the file (e.g., image/jpeg, application/pdf)
    public Stream Content { get; set; } // The file content to be uploaded
}
