using MedLog.Domain.Enums;

namespace MedLog.Service.DTOs.AppointmentDTOs;

public class AppointmentUpdateDto 
{
    public DateTime AppointmentDateTime { get; set; }
    public AppointmentType? AppointmentType { get; set; }
    public string? AppointmentNotes { get; set; }
    public bool IsConfirmed { get; set; }
}
