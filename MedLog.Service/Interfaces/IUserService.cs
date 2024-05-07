using MedLog.Service.DTOs.DoctorDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MongoDB.Bson;

namespace MedLog.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreatePatientAsync(UserCreationDto userDto, string selectedHospitalId);
    Task<UserResultDto> CreateDoctorAsync(DoctorCreationDto dto, string selectedHospitalId);
    Task<UserResultDto> CreateNurseAsync(NurseCreationDto dto, string selectedHospitalId);

    Task<UserResultDto> UpdateAsync(string id, UserUpdateDto dto);
    Task<bool> DeleteAsync (string id);
    Task<UserResultDto> GetAsync (string id);
    Task<bool> IsDoctorAvailableAtTimeAsync(DateTime appointmentDateTime, string doctorId);
    Task<List<DoctorDto>> GetDoctorsByHospitalId (string hospitalId);
    Task<List<UserResultDto>> GetAllAsync ();
}
