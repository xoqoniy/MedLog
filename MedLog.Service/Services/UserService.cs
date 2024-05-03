using AutoMapper;
using AutoMapper.Internal;
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
    private readonly IHospitalService hospitalService;
    private readonly IMapper mapper;

    public UserService(IRepository<User> repository, IMapper mapper, IHospitalService hospitalService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.hospitalService = hospitalService;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto userDto, string selectedHospitalId)
    {
        try
        {
            // Generate an ObjectId for the address if needed
            string addressId = ObjectId.GenerateNewId().ToString();

            // Map the UserCreationDto to User entity
            var newUser = mapper.Map<User>(userDto);

            // Assign the generated address ID to the address
            newUser.Address._id = addressId;

            // Assign the selected hospital ID to the user's HospitalId property
            newUser.HospitalId = selectedHospitalId;

            // Save the User to obtain its ID
            await repository.InsertAsync(newUser);
            
            // Retrieve the hospital entity by its ID
            await hospitalService.AddUserIdToHospital(selectedHospitalId, newUser._id);

            // Map the created user to UserResultDto
            return mapper.Map<UserResultDto>(newUser);

        }
        catch (Exception ex)
        {
            // Handle any exceptions
            throw new MedLogException(500, $"Failed to create user -> {ex.Message}");
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await repository.RetrieveByIdAsync(id);
        if(user is null)
            throw new MedLogException(404, "User not found");
        
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
            throw new MedLogException(404, "User not found");
        
        return mapper.Map<UserResultDto>(user);
    }

    //public Task<List<HospitalResultDto>> GetHospitalsInCity(string city)
    //{
    //    return await _hospitalRepository.GetAsync(h => h.Address.City == city);
    //}

    public async Task<UserResultDto> UpdateAsync(string id, UserUpdateDto dto)
    {
        var user = await repository.RetrieveByIdAsync(id);

        if(user is null)
            throw new MedLogException(404, "User not found");
        

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
