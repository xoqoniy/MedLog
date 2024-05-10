using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.Exceptions;
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
    public async Task AddUserIdToHospital(string hospitalId, string newUserId)
    {
        try
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
        catch (Exception ex)
        {
            // Handle any exceptions
            throw new Exception($"Failed to add user ID to hospital: {ex.Message}");
        }
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
        try
        {
            await repository.RemoveByIdAsync(id);
            return true;
        }
        catch(Exception ex)
        {
            throw new MedLogException(404, ex.Message);
        }
    }

    public async Task<List<HospitalResultDto>> GetAllAsync()
    {
        try
        {
            var hospitals = await repository.RetrieveAllAsync();
            return mapper.Map<List<HospitalResultDto>>(hospitals);
        }
        catch(MedLogException ex) 
        {
            throw new MedLogException(404, ex.Message);
        }
    }

    public async Task<HospitalResultDto> GetByIdAsync(string id)
    {
        try
        {
            var hospital = await repository.RetrieveByIdAsync(id);
            return mapper.Map<HospitalResultDto>(hospital);
        }
        catch(MedLogException ex)
        {
            throw new MedLogException(404, ex.Message);
        }
    }

    public async Task<List<string>> GetHospitalsInCity(string city)
    {
        try
        {
            // Define the expression to filter hospitals by city (case-insensitive)
            Expression<Func<Hospital, bool>> expression = h => h.Address.City.ToLower() == city.ToLower();

            // Retrieve hospitals using the generic repository method
            var hospitals = await repository.RetrieveByExpressionAsync(expression);

            // Map hospitals' names to a list of strings
            var hospitalNames = hospitals.Select(h => h.Name).ToList();

            return hospitalNames;
        }
        catch (MedLogException ex)
        {
            throw new MedLogException(404, ex.Message);
        }
    }


    public async Task<HospitalResultDto> UpdateAsync(string id, HospitalUpdateDto dto)
    {
        try
        {
            var hospital = await repository.RetrieveByIdAsync(id);
            mapper.Map(dto, hospital);
            hospital.LastUpdatedAt = DateTime.UtcNow;
            await repository.ReplaceByIdAsync(hospital);
            return mapper.Map<HospitalResultDto>(hospital);
        }
        catch( MedLogException ex)
        {
            throw new MedLogException(404, ex.Message);
        }
    }
}
