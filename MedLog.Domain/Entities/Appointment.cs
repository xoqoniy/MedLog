using MedLog.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities
{
    public class Appointment : Auditable
    {
        public string? UserId { get; set; }  // ID of the patient booking the appointment

        // Navigation property for easy retrieval of associated patient information
        [BsonIgnore]
        public User? User { get; set; }

        public string? StaffId { get; set; }  // ID of the staff (doctor) handling the appointment

        // Navigation property for easy retrieval of associated staff information
        [BsonIgnore]
        public Staff? Staff { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentType { get; set; }  // e.g., "Routine Checkup", "Follow-up", "Procedure"
        public string AppointmentNotes { get; set; }  // Additional notes about the appointment
        public bool IsConfirmed { get; set; } = false; 
       
    }

}
