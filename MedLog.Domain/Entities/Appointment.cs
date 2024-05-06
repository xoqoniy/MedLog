using MedLog.Domain.Common;
using MedLog.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities
{
    public class Appointment : Auditable
    {

        public string PatientId { get; set; } // ID of the patient user
        public string DoctorId { get; set; } // ID of the doctor user
        public DateTime AppointmentDateTime { get; set; }
        public AppointmentType? AppointmentType { get; set; } = Enums.AppointmentType.RoutineCheck;
        public string? AppointmentNotes { get; set; }  // Additional notes about the appointment
        public bool IsConfirmed { get; set; } = false;
    }

}
