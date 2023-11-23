using MongoDB.Driver;
using MedLog.Domain.Common;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;

using MedLog.DAL.DbContexts;
using Microsoft.Extensions.Options;

namespace MedLog.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable{ 

    //private const string collectionName = "items";
    private readonly IMongoCollection<T> _collection;
    private readonly FilterDefinitionBuilder<User> _filterBuilder = Builders<User>.Filter;

    public Repository(IOptions<MongoDbSettings> mongodbSettings)
    {
        MongoClient client = new(mongodbSettings.Value.ConnectionStringURL);
        IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
        //_collection = database.GetCollection<T>(mongodbSettings.Value.CollectionName);

        var collectionName = GetCollectionName<T>(mongodbSettings);
        _collection = database.GetCollection<T>(collectionName);
        
    }

    private string GetCollectionName<T>(IOptions<MongoDbSettings> mongodbSettings)
    {
        // Use the type of T to determine the appropriate collection name
        return typeof(T) switch
        {
            Type t when t == typeof(User) => mongodbSettings.Value.UsersCollection,
            Type t when t == typeof(Staff) => mongodbSettings.Value.StaffCollection,
            // Add more cases for other entities as needed
            _ => throw new NotSupportedException($"Unsupported entity type: {typeof(T)}"),
        };
    }

    public async Task<T> CreateAsync(T user)
    {
        await _collection.InsertOneAsync(user);
        return user;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
    }

    public async Task<T> GetAsync(string id)
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(entity => entity.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public Task<bool> RemoveAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    //public async Task<List<User>> GetAllAsync()
    //{
    //    return await _userscollection.Find(_filterBuilder.Empty).ToListAsync();
    //}

    //public async Task<User> GetAsync(string id)
    //{
    //    FilterDefinition<User> filter = _filterBuilder.Eq(entity => entity.Id, id);
    //    return await _userscollection.Find(filter).FirstOrDefaultAsync();
    //}

    //public async Task<User> CreateAsync(User user)
    //{
    //    await _userscollection.InsertOneAsync(user);
    //    return user;

    //}

    //public async Task UpdateAsync(User user)
    //{
    //    if (user is null)
    //    {
    //        throw new ArgumentNullException(nameof(user));
    //    }
    //    FilterDefinition<User> filter = _filterBuilder.Eq(existingentity => existingentity.Id, user.Id);
    //    await _userscollection.ReplaceOneAsync(filter, user);
    //}

    //public async Task<bool> RemoveAsync(string id)
    //{
    //    FilterDefinition<User> filter = _filterBuilder.Eq(entity => entity.Id, id);
    //    await _userscollection.DeleteOneAsync(filter);
    //    return true;
    //}
}
