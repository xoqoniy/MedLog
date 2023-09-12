

using MedLog.Domain.Common;

namespace MedLog.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> RemoveAsync(int id);
}
