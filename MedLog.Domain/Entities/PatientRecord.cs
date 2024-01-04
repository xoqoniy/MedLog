using MedLog.Domain.Common;
using MongoDB.Bson;

namespace MedLog.Domain.Entities;

public class PatientRecord : Auditable, IEntityWithObjectId
{
    public Staff? Staff { get; set; }
    public User? User { get; set; }
    public ObjectId FileId { get; set; } // ObjectId of the file in GridFS
    public string Diagnosis { get; set; }
    public string Symptoms { get; set; }
    public string Medications { get; set; }
    public string Procedures { get; set; }
    public string Allergies { get; set; }
    public string Description { get; set; }

}
