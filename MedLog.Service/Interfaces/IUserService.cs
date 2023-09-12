

using MedLog.Service.DTOs.UserDTOs;

namespace MedLog.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(UserCreationDto user);
    Task<UserResultDto> UpdateAsync(UserUpdateDto user);
    Task<bool> DeleteAsync (int id);
    Task<UserResultDto?> GetAsync (int id);
    Task GetAllAsync ();
}
