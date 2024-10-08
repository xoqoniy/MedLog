﻿
using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Configurations;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.PatientRecordDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Extensions;
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
    public async Task<PatientRecordResultDto> CreateAsync(string patientId, PatientRecordCreationDto dto)
    {
        if (patientId != null)
        {
            var patientRecord = mapper.Map<PatientRecord>(dto);
            patientRecord.PatientId = patientId;
            await repository.InsertAsync(patientRecord);
            return mapper.Map<PatientRecordResultDto>(patientRecord);
        }
        else
        {
            throw new MedLogException(403, "PatientId didn't match");
        }
    }

    public async Task<bool> DeleteAsync(string patientRecordId)
    {
        var patientRecord = await repository.RetrieveByIdAsync(patientRecordId);
        if (patientRecord is null)
            throw new MedLogException(404, "Record not found");

        await repository.RemoveByIdAsync(patientRecordId);
        return true;
    }

    public async Task<PaginationResult<PatientRecordResultDto>> GetAllPatientRecordsAsync(PaginationParams @params)
    {
        var patientRecordsQuery = repository.SelectAll();
        var pagedPatientRecords = await patientRecordsQuery.ToPagedListAsync(@params);
        var result = mapper.Map<List<PatientRecordResultDto>>(pagedPatientRecords.Data);

        return new PaginationResult<PatientRecordResultDto>(result, pagedPatientRecords.TotalCount, @params.PageIndex, @params.PageSize);
    }

    public async Task<PatientRecordResultDto> GetPatientRecordById(string recordId)
    {
        var patientRecord = await repository.RetrieveByIdAsync(recordId);
        return mapper.Map<PatientRecordResultDto>(patientRecord);
    }

    public async Task<List<PatientRecordResultDto>> GetPatientRecordsById(string patientId)
    {
        var patientRecords = await repository.RetrieveByExpressionAsync(p => p.PatientId == patientId);
        return mapper.Map<List<PatientRecordResultDto>>(patientRecords);
    }

    public async Task<PatientRecordResultDto> UpdateAsync(string patientRecordId, PatientRecordUpdateDto dto)
    {
        var patientRecord = await repository.RetrieveByIdAsync(patientRecordId);

        if (patientRecord is null)
            throw new MedLogException(404, "Record not found");


        // Map the properties from the update DTO to the existing user
        mapper.Map(dto, patientRecord);

        // Update additional properties if needed

        patientRecord.LastUpdatedAt = DateTime.UtcNow;

        // Update the user
        await repository.ReplaceByIdAsync(patientRecord);

        // Return the updated user DTO
        return mapper.Map<PatientRecordResultDto>(patientRecord);
    }
}
