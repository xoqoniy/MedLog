using MedLog.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities;

public class PatientRecord : Auditable
{
    public Staff? Staff { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string StaffId { get; set; }
    public User? User { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]

    public string UserId { get; set; }
    public string Diagnosis { get; set; }
    public string Symptoms { get; set; }
    public string Medications { get; set; }
    public string Procedures { get; set; }
    public string Allergies { get; set; }
    public string Description { get; set; }

}
