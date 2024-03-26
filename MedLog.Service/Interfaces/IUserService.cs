using MedLog.Service.DTOs.UserDTOs;
using MongoDB.Bson;

namespace MedLog.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreateAsync(UserCreationDto user);
    Task<UserResultDto> UpdateAsync(string _id);
    Task<bool> DeleteAsync (string id);
    Task<UserResultDto> GetAsync (string id);
    Task<List<UserResultDto>> GetAllAsync ();
}
