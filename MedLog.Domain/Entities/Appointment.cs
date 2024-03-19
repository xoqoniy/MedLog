using MedLog.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities
{
    public class Appointment : Auditable
    {
        public string? UserId { get; set; }  // ID of the patient booking the appointment
        [BsonIgnore]
        public User? User { get; set; } // Navigation property for easy retrieval of associated patient information
        public string? StaffId { get; set; }  // ID of the staff (doctor) handling the appointment
        [BsonIgnore]
        public Staff? Staff { get; set; } // Navigation property for easy retrieval of associated staff information
        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentType { get; set; }  // e.g., "Routine Checkup", "Follow-up", "Procedure"
        public string? AppointmentNotes { get; set; }  // Additional notes about the appointment
        public bool IsConfirmed { get; set; } = false;
        public List<PatientRecord> MedicalRecords { get; set; } // List of associated patient medical records
    }

}
