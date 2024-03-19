using MedLog.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities;

public class PatientRecord : Auditable
{
    public User Patient { get; set; } // Reference to the patient (User entity)
    public string FileName { get; set; } // Name of the file
    public long FileSize { get; set; } // Size of the file
    public string? FileType { get; set; } // Type of the file (e.g., PDF, image, etc.)
    public string? Description { get; set; } // Description of the record
    public ObjectId FileId { get; set; } // ObjectId of the file in GridFS
}
