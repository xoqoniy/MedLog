using MedLog.Service.DTOs.UserDTOs;
using MongoDB.Bson;

namespace MedLog.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(UserCreationDto user);
    Task<UserResultDto> UpdateAsync(ObjectId id, UserUpdateDto dto);
    Task<bool> DeleteAsync (ObjectId id);
    Task<UserResultDto> GetAsync (ObjectId id);
    Task<List<UserResultDto>> GetAllAsync ();
}
