namespace MedLog.Service.DTOs.FileDTOs
{
    public class FileResultDto
    {
        public string Id { get; set; } // File ID
        public string UserId { get; set; } // User ID (patient ID) associated with the file
        public string FileName { get; set; } // Name of the file
        public string ContentType { get; set; } // Content type of the file (e.g., image/jpeg, application/pdf)
        public long Length { get; set; } // Size of the file in bytes
        public Stream FileStream { get; set; } // The file content to be uploaded
        public DateTime CreatedAt { get; set; } // File creation date
    }
}
