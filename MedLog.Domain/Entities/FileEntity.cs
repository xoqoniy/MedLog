using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedLog.Domain.Common;

namespace MedLog.Domain.Entities;

public class FileEntity : Auditable
{

    public string UserId { get; set; } // User ID (patient ID) associated with the file

    public string FileName { get; set; } // Name of the file
    public string? Description { get; set; }

    public string ContentType { get; set; } // Content type of the file (e.g., image/jpeg, application/pdf)

    public Stream Content {  get; set; }
    public long Length { get; set; } // Size of the file in bytes

    // Additional metadata properties as needed

    // You can also include properties for storing the file content if necessary,
    // although in most cases, the file content will be stored directly in GridFS
    // and not in the entity itself.
}
