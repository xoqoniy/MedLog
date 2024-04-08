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
    private readonly IHospitalService hospitalService;
    private readonly IMapper mapper;

    public UserService(IRepository<User> repository, IMapper mapper, IHospitalService hospitalService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.hospitalService = hospitalService;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto userDto)
    {
        try
        {
            // Generate an ObjectId for the address
            string addressId = ObjectId.GenerateNewId().ToString();

            // Map the UserCreationDto to User entity
            var newUser = mapper.Map<User>(userDto);

            // Assign the generated address ID to the user's address
            newUser.Address._id = addressId;

            // Retrieve hospitals in the user's city
            var hospitalsInCity = await hospitalService.GetHospitalsInCity(newUser.Address.City);

            // Map hospitals to HospitalResultDto for frontend selection
            var hospitalsDto = mapper.Map<List<HospitalResultDto>>(hospitalsInCity);

            // Convert HospitalResultDto list back to Hospital entities
            var hospitals = mapper.Map<List<Hospital>>(hospitalsInCity);

            // Assign the list of hospitals to the user's AvailableHospitals
            newUser.AvailableHospitals = hospitals;

            // Save the user to the repository
            await repository.InsertAsync(newUser);

            // Map the created user to UserResultDto
            var userResultDto = mapper.Map<UserResultDto>(newUser);
            userResultDto.AvailableHospitals = hospitalsDto;

            return userResultDto;
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            throw new Exception("Failed to create user.", ex);
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
