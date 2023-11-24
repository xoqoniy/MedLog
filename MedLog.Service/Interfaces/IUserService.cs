using MedLog.Service.DTOs.UserDTOs;

namespace MedLog.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(UserCreationDto user);
    Task<UserResultDto> UpdateAsync(string id, UserUpdateDto dto);
    Task<bool> DeleteAsync (string id);
    Task<UserResultDto> GetAsync (string id);
    Task<List<UserResultDto>> GetAllAsync ();
}
