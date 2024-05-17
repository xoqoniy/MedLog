namespace MedLog.Service.DTOs.FileDTOs;
#pragma warning disable 
public class FileUpdateDto
{
    public string Id { get; set; } // File ID
    public string FileName { get; set; } // Updated name of the file
    public string ContentType { get; set; } // Updated content type of the file (e.g., image/jpeg, application/pdf)
    public string UserId { get; set; } // Updated User ID (patient ID) associated with the file
}
