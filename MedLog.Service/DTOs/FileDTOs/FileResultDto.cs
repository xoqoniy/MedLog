namespace MedLog.Service.DTOs.FileDTOs
{
    public class FileResultDto
    {
        public string _id { get; set; } // File ID
        public string UserId { get; set; } // User ID (patient ID) associated with the file
        public string FileName { get; set; } // Name of the file
        public string Description { get; set; }
        public string ContentType { get; set; } // Content type of the file (e.g., image/jpeg, application/pdf)
        public Stream Content { get; set; }
        public long Length { get; set; } // Size of the file in bytes
        public DateTime CreatedAt { get; set; } // File creation date
    }
}
