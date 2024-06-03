using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Configurations;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Extensions;
using MedLog.Service.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Archives;
using System.Linq.Expressions;

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
        string addressId = ObjectId.GenerateNewId().ToString();
        var mapped = mapper.Map<Hospital>(dto);
        mapped.Address._id = addressId;
        await repository.InsertAsync(mapped);
        return mapper.Map<HospitalResultDto>(mapped);
    }
    public async Task AddUserIdToHospital(string hospitalId, string newUserId)
    {
        // Retrieve the hospital entity by its ID
        var hospital = await repository.RetrieveByIdAsync(hospitalId);

        if (hospital != null)
        {
            // Add the new user ID to the existing collection
            hospital.UserIds.Add(newUserId);

            // Create an update definition to set the modified UserIds field
            var updateDefinition = Builders<Hospital>.Update
                .Set(h => h.UserIds, hospital.UserIds);

            // Perform the partial update using the PartialUpdateAsync method
            await repository.PartialUpdateAsync(hospitalId, updateDefinition);
        }
        else
        {
            // Handle the case where the hospital entity is not found
            throw new Exception($"Hospital with ID '{hospitalId}' not found.");
        }
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
        await repository.RemoveByIdAsync(id);
        return true;
    }

    public async Task<PaginationResult<HospitalResultDto>> GetAllHospitalsAsync(PaginationParams @params)
    {
        var hospitalsQuery = repository.SelectAll();
        var pagedHospitals = await hospitalsQuery.ToPagedListAsync(@params);
        var result = mapper.Map<List<HospitalResultDto>>(pagedHospitals.Data);

        return new PaginationResult<HospitalResultDto>(result, pagedHospitals.TotalCount, @params.PageIndex, @params.PageSize);
    }

    public async Task<HospitalResultDto> GetByIdAsync(string id)
    {
        var hospital = await repository.RetrieveByIdAsync(id);
        return mapper.Map<HospitalResultDto>(hospital);
    }

    public async Task<List<string>> GetHospitalsInCity(string city)
    {
        // Define the expression to filter hospitals by city (case-insensitive)
        Expression<Func<Hospital, bool>> expression = h => h.Address.City.ToLower() == city.ToLower();

        // Retrieve hospitals using the generic repository method
        var hospitals = await repository.RetrieveByExpressionAsync(expression);

        // Map hospitals' names to a list of strings
        var hospitalNames = hospitals.Select(h => h.Name).ToList();

        return hospitalNames;
    }


    public async Task<HospitalResultDto> UpdateAsync(string id, HospitalUpdateDto dto)
    {
        var hospital = await repository.RetrieveByIdAsync(id);
        mapper.Map(dto, hospital);
        hospital.LastUpdatedAt = DateTime.UtcNow;
        await repository.ReplaceByIdAsync(hospital);
        return mapper.Map<HospitalResultDto>(hospital);
    }
}
