using MedLog.Domain.Configurations;
using MedLog.Service.DTOs.AppointmentDTOs;

namespace MedLog.Service.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResultDto> CreateAsync(AppointmentCreationDto dto, string patientId, string doctorId);
    Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto, string appointmentId);
    Task<bool> DeleteByAppointmentIdAsync(string appointmentId);
    Task<PaginationResult<AppointmentResultDto>> GetAllAppointmentsAsync(PaginationParams @params);
    Task<List<AppointmentResultDto>> GetAppointmentsByPatientIdAsync(string patientId);
    Task<List<AppointmentResultDto>> GetAppointmentsByDoctorIdAsync(string doctorId);
}

