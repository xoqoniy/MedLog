

using MedLog.Domain.Common;
using MedLog.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace MedLog.DAL.IRepositories;

public interface IRepository<T> where T : Auditable
{
    public IMongoQueryable<T> SelectAll(
     Expression<Func<T, bool>> expression = null);
    Task<T> RetrieveByIdAsync(string id);
    Task<T> InsertAsync(T entity);
    Task ReplaceByIdAsync(T entity);
    Task PartialUpdateAsync(string id, UpdateDefinition<T> updateDefinition);
    Task<bool> RemoveByIdAsync(string id);
    Task<List<T>> RetrieveByExpressionAsync(Expression<Func<T, bool>> expression);
    
    
}
