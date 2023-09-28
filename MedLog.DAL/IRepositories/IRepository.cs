

using MedLog.Domain.Common;
using MedLog.Domain.Entities;

namespace MedLog.DAL.IRepositories;

public interface IRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> GetAsync(string id);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User entity);
    Task<bool> RemoveAsync(string id);
}
