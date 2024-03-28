using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;
using MongoDB.Bson;

namespace MedLog.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;

    public UserService(IRepository<User> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto user)
    {
        //Generate ID for the address(embedded class on User entity)
        string addressId = ObjectId.GenerateNewId().ToString();

        //Map the UserCreationDto to User 
        var mapped = mapper.Map<User>(user);

        //Assign the generated Id to the Address entity
        mapped.Address._id = addressId;

        //Save the User with the mapped Address
        await repository.InsertAsync(mapped);

        //Return the mapped UserResultDto
        return mapper.Map<UserResultDto>(mapped);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await repository.RetrieveByIdAsync(id);
        if (user is null)
        {
            throw new MedLogException(404, "User not found");
        }
        await repository.RemoveByIdAsync(id);
        return true;
    }

    public async Task<List<UserResultDto>> GetAllAsync()
    {
        var users = await repository.RetrieveAllAsync();
        return mapper.Map<List<UserResultDto>>(users);
    }

    public async Task<UserResultDto> GetAsync(string id)
    {
        var user = await repository.RetrieveByIdAsync(id);
        if(user is null)
        {
            throw new MedLogException(404, "User not found");
        }
        return mapper.Map<UserResultDto>(user);
    }

    //public Task<List<HospitalResultDto>> GetHospitalsInCity(string city)
    //{
    //    return await _hospitalRepository.GetAsync(h => h.Address.City == city);
    //}

    public async Task<UserResultDto> UpdateAsync(string id, UserUpdateDto dto)
    {
        var user = await repository.RetrieveByIdAsync(id);

        if (user is null)
        {
            throw new MedLogException(404, "User not found");
        }

        // Map the properties from the update DTO to the existing user
        mapper.Map(dto, user);

        // Update additional properties if needed

        user.LastUpdatedAt = DateTime.UtcNow;

        // Perform server-side validation if needed

        // Update the user
        await repository.ReplaceByIdAsync(user);

        // Return the updated user DTO
        return mapper.Map<UserResultDto>(user);
    }
}
