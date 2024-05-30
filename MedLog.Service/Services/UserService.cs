using AutoMapper;
using AutoMapper.Internal;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.DoctorDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;
using MongoDB.Bson;
using System.Text.Json;

namespace MedLog.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> repository;
    private readonly IRepository<Appointment> appointmentrepo;
    private readonly IHospitalService hospitalService;
    private readonly IMapper mapper;

    public UserService(IRepository<User> repository, IRepository<Appointment> appointmentrepo, IMapper mapper, IHospitalService hospitalService)
    {
        this.appointmentrepo = appointmentrepo;
        this.repository = repository;
        this.mapper = mapper;
        this.hospitalService = hospitalService;
    }

    public async Task<UserResultDto> CreatePatientAsync(UserCreationDto userDto, string selectedHospitalId)
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

    public async Task<UserResultDto> CreateDoctorAsync(DoctorCreationDto dto, string selectedHospitalId)
    {
        // Generate an ObjectId for the address if needed
        string addressId = ObjectId.GenerateNewId().ToString();

        // Map the UserCreationDto to User entity
        var newUser = mapper.Map<User>(dto);

        // Assign the generated address ID to the address
        newUser.Address._id = addressId;
        newUser.UserRole = Role.Doctor;

        // Assign the selected hospital ID to the user's HospitalId property
        newUser.HospitalId = selectedHospitalId;

        // Save the User to obtain its ID
        await repository.InsertAsync(newUser);

        await hospitalService.AddUserIdToHospital(selectedHospitalId, newUser._id);
        // Map the created user to UserResultDto
        return mapper.Map<UserResultDto>(newUser);

    }

    public async Task<UserResultDto> CreateNurseAsync(NurseCreationDto dto, string selectedHospitalId)
    {
        // Generate an ObjectId for the address if needed
        string addressId = ObjectId.GenerateNewId().ToString();

        // Map the UserCreationDto to User entity
        var newUser = mapper.Map<User>(dto);

        // Assign the generated address ID to the address
        newUser.Address._id = addressId;

        newUser.UserRole = Role.Nurse;

        // Assign the selected hospital ID to the user's HospitalId property
        newUser.HospitalId = selectedHospitalId;

        // Save the User to obtain its ID
        await repository.InsertAsync(newUser);

        // Retrieve the hospital entity by its ID
        await hospitalService.AddUserIdToHospital(selectedHospitalId, newUser._id);

        // Map the created user to UserResultDto
        return mapper.Map<UserResultDto>(newUser);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var user = await repository.RetrieveByIdAsync(id);
            if (user is null)
                throw new MedLogException(404, "User not found");

            await repository.RemoveByIdAsync(id);
            return true;
        }
        catch(Exception ex)
        {
            throw new MedLogException(404, "User wasn't deleted " + ex.Message);
        }

    }

    public async Task<List<UserResultDto>> GetAllAsync()
    {
        var users = await repository.RetrieveAllAsync();
        return mapper.Map<List<UserResultDto>>(users);
    }

    public async Task<UserResultDto> GetAsync(string id)
    {
        var user = await repository.RetrieveByIdAsync(id);
        return mapper.Map<UserResultDto>(user);
    }


    public async Task<List<UserResultDto>> GetNursesByHospitalId (string hospitalId)
    {
        var nurses = await repository.RetrieveByExpressionAsync(u =>
                u.UserRole == Role.Nurse && u.HospitalId == hospitalId);
        return mapper.Map<List<UserResultDto>>(nurses);
    }
    public async Task<List<DoctorDto>> GetDoctorsByHospitalId(string hospitalId)
    {
        var doctors = await repository.RetrieveByExpressionAsync(u =>
                        u.UserRole == Role.Doctor && u.HospitalId == hospitalId);

        var doctorDtos = doctors.Select(d => new DoctorDto
        {
            Id = d._id,
            FullName = $"{d.FirstName} {d.LastName}",
            Specialization = d.Specialization
        }).ToList();
        return doctorDtos;
    }

    public async Task<UserResultDto> UpdateAsync(string id, UserUpdateDto dto)
    {
        var user = await repository.RetrieveByIdAsync(id);

        if (user is null)
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

    public async Task<bool> IsDoctorAvailableAtTimeAsync(DateTime appointmentDateTime, string doctorId)
    {
        // Define the end time for the time window (e.g., 10 minutes after the chosen appointment time)
        DateTime endTime = appointmentDateTime.AddMinutes(10);

        // Query the doctor's appointments within the specified time window
        var overlappingAppointments = await appointmentrepo.RetrieveByExpressionAsync(appointment =>
            appointment.DoctorId == doctorId &&
            appointment.AppointmentDateTime < endTime &&
            appointment.AppointmentDateTime >= appointmentDateTime &&
            appointment.IsConfirmed);

        // Check if any overlapping appointments are found
        return !overlappingAppointments.Any();
    }


}
