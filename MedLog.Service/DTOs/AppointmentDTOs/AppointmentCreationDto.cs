using MedLog.Domain.Enums;
using System.Text.Json.Serialization;

namespace MedLog.Service.DTOs.AppointmentDTOs;

public class AppointmentCreationDto
{
    [JsonIgnore]
    public string PatientId { get; set; } // Id of the patient user
    [JsonIgnore]
    public string DoctorId { get; set; } // Id of the doctor user
    public DateTime AppointmentDateTime { get; set; }
    public AppointmentType AppointmentType { get; set; } = Domain.Enums.AppointmentType.RoutineCheck;
    public string? AppointmentNotes { get; set; }
        
    [JsonIgnore]
    public bool IsConfirmed { get; set; }
}
