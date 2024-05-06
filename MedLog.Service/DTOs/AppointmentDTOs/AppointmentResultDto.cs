﻿using MedLog.Domain.Enums;

namespace MedLog.Service.DTOs.AppointmentDTOs;

public class AppointmentResultDto
{
    public string Id { get; set; }
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public AppointmentType? AppointmentType { get; set; }
    public string? AppointmentNotes { get; set; }
    public bool IsConfirmed { get; set; }

}
