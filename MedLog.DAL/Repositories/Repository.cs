using MongoDB.Driver;
using MedLog.Domain.Common;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;

using MedLog.DAL.DbContexts;
using Microsoft.Extensions.Options;

namespace MedLog.DAL.Repositories;

public class Repository : IRepository{ 

    //private const string collectionName = "items";
    private readonly IMongoCollection<User> _userscollection;
    private readonly FilterDefinitionBuilder<User> _filterBuilder = Builders<User>.Filter;

    public Repository(IOptions<MongoDbSettings> mongodbSettings)
    {
        MongoClient client = new(mongodbSettings.Value.ConnectionStringURL);
        IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
        _userscollection = database.GetCollection<User>(mongodbSettings.Value.CollectionName);
        
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userscollection.Find(_filterBuilder.Empty).ToListAsync();
    }

    public async Task<User> GetAsync(string id)
    {
        FilterDefinition<User> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return await _userscollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _userscollection.InsertOneAsync(user);
        return user;
        
    }

    public async Task UpdateAsync(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        FilterDefinition<User> filter = _filterBuilder.Eq(existingentity => existingentity.Id, user.Id);
        await _userscollection.ReplaceOneAsync(filter, user);
    }

    public async Task<bool> RemoveAsync(string id)
    {
        FilterDefinition<User> filter = _filterBuilder.Eq(entity => entity.Id, id);
        await _userscollection.DeleteOneAsync(filter);
        return true;
    }
}
