
using MedLog.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities
{
    public class MedicalHistory : Auditable
    {
        public string UserId { get; set; }
        [BsonIgnore]
        public User User { get; set; }
        public string StaffId { get; set; }
        public Staff Staff { get; set; }

        public List<PatientRecord> PatientRecords { get; set; } = new List<PatientRecord>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

       
    }

}
