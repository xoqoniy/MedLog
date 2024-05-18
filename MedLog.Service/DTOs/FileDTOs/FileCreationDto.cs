namespace MedLog.Service.DTOs.FileDTOs;
#pragma warning disable 
public class FileCreationDto
{
    public string UserId { get; set; } // User ID (patient ID) associated with the file
    public string FileName { get; set; } // Name of the file
    public string ContentType { get; set; } // Content type of the file (e.g., image/jpeg, application/pdf)
    public Stream FileStream { get; set; } // The file content to be uploaded
}
