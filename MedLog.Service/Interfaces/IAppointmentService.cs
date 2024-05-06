
using MedLog.Service.DTOs.AppointmentDTOs;

namespace MedLog.Service.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResultDto> CreateAsync(AppointmentCreationDto dto, string patientId);
    Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto, string patientId);
    Task<bool> DeleteAsync(string appointmentId);
    Task<List<AppointmentResultDto>> GetAllAsync();
    Task<AppointmentResultDto> GetByPatientIdAsync(string patientId);
}

