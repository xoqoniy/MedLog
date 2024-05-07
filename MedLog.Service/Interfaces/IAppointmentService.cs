
using MedLog.Service.DTOs.AppointmentDTOs;

namespace MedLog.Service.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResultDto> CreateAsync(AppointmentCreationDto dto, string patientId);
    Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto, string appointmentId);
    Task<bool> DeleteByAppointmentIdAsync(string appointmentId);
    Task<List<AppointmentResultDto>> GetAllAppointmentsAsync();
    Task<List<AppointmentResultDto>> GetAppointmentsByPatientIdAsync(string patientId);
    Task<List<AppointmentResultDto>> GetAppointmentsByDoctorIdAsync(string doctorId);
}

