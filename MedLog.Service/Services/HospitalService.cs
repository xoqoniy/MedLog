
using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.Interfaces;
using MongoDB.Bson;

namespace MedLog.Service.Services;

public class HospitalService : IHospitalService
{
    private readonly IRepository<Hospital> repository;
    private readonly IMapper mapper;

    public HospitalService(IRepository<Hospital> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<HospitalResultDto> CreateAsync(HospitalCreationDto dto)
    {
        try
        {
            string addressId = ObjectId.GenerateNewId().ToString();
            var mapped = mapper.Map<Hospital>(dto);
            mapped.Address._id = addressId;
            await repository.InsertAsync(mapped);
            return mapper.Map<HospitalResultDto>(mapped);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }  
    }

    public Task<bool> DeleteByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<HospitalResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<HospitalResultDto> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<HospitalResultDto>> GetHospitalsInCity(string city)
    {
        throw new NotImplementedException();
    }

    public Task<HospitalResultDto> UpdateAsync(HospitalUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
