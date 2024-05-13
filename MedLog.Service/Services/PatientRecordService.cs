
using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.PatientRecordDTOs;
using MedLog.Service.Interfaces;

namespace MedLog.Service.Services;

public class PatientRecordService : IPatientRecordService
{
    private readonly IRepository<PatientRecord> repository;
    private readonly IMapper mapper;

    public PatientRecordService(IRepository<PatientRecord> _repository, IMapper _mapper)
    {
        this.mapper = _mapper;
        this.repository = _repository;
    }
    public Task<PatientRecordResultDto> CreateAsync(string patientId, PatientRecordCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string patientId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PatientRecordResultDto>> GetAllPatientRecords()
    {
        throw new NotImplementedException();
    }

    public Task<PatientRecordResultDto> GetAsync(string patientId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PatientRecordResultDto>> GetPatientRecordsById(string patientId)
    {
        throw new NotImplementedException();
    }

    public Task<PatientRecordResultDto> UpdateAsync(string patientId, PatientRecordUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
