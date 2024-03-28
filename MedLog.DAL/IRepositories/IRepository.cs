

using MedLog.Domain.Common;
using MedLog.Domain.Entities;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace MedLog.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task<List<T>> RetrieveAllAsync();
    Task<T> RetrieveByIdAsync(string id);
    Task<T> InsertAsync(T entity);
    Task ReplaceByIdAsync(T entity);
    Task<bool> RemoveByIdAsync(string id);
    Task<List<T>> RetrieveByExpressionAsync(Expression<Func<T, bool>> expression);
    
    
}
