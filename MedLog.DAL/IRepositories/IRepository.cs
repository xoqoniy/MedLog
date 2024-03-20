

using MedLog.Domain.Common;
using MedLog.Domain.Entities;
using MongoDB.Bson;

namespace MedLog.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(ObjectId id);
    Task<T> CreateAsync(T user);
    Task UpdateAsync(T entity);
    Task<bool> RemoveAsync(ObjectId id);
}
