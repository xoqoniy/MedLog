

using MedLog.Domain.Common;

namespace MedLog.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(string id);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> RemoveAsync(string id);
}
