using MedLog.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities
{
    public class Appointment : Auditable
    {
       
        [BsonIgnore]
        public User User { get; set; } // Navigation property for easy retrieval of associated patient information
        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentType { get; set; }  // e.g., "Routine Checkup", "Follow-up", "Procedure"
        public string? AppointmentNotes { get; set; }  // Additional notes about the appointment
        public bool IsConfirmed { get; set; } = false;
    }

}
