using MongoDB.Driver;
using MedLog.Domain.Common;
using System.Runtime.CompilerServices;
using MedLog.DAL.IRepositories;

namespace MedLog.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable { 

    private const string collectionName = "items";
    private readonly IMongoCollection<T> _collection;
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

    public Repository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:7103");
        var database = mongoClient.GetDatabase("MedLog");
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_filterBuilder.Empty).ToListAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
        
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity is 0)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        FilterDefinition<T> filter = _filterBuilder.Eq(existingentity => existingentity.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task<bool> RemoveAsync(int id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
        await _collection.DeleteOneAsync(filter);
        return true;
    }
}
