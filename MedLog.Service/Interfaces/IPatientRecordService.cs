using MedLog.Domain.Configurations;
using MedLog.Service.DTOs.PatientRecordDTOs;
using System;

namespace MedLog.Service.Interfaces;

public interface IPatientRecordService
{
    Task<PatientRecordResultDto> CreateAsync(string patientId, PatientRecordCreationDto dto);
    Task<PatientRecordResultDto> UpdateAsync(string patientId, PatientRecordUpdateDto dto);
    Task<bool> DeleteAsync(string patientId);
    Task<PatientRecordResultDto> GetPatientRecordById(string recordId);
    Task<List<PatientRecordResultDto>> GetPatientRecordsById(string patientId);
    Task<PaginationResult<PatientRecordResultDto>> GetAllPatientRecordsAsync(PaginationParams @params);
}
