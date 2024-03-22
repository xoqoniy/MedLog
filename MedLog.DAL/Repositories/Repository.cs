using MongoDB.Driver;
using MedLog.Domain.Common;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;

using MedLog.DAL.DbContexts;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace MedLog.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    // MongoDB collection for the specified entity type T
    private readonly IMongoCollection<T> _collection;

    // Filter builder for creating MongoDB filter definitions
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

    // Constructor that takes MongoDB settings as a dependency
    public Repository(IOptions<MongoDbSettings> mongodbSettings)
    {
        // Create a MongoDB client using the connection string from settings
        MongoClient client = new(mongodbSettings.Value.ConnectionStringURL);

        // Get the MongoDB database using the database name from settings
        IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);

        // Determine the collection name based on the entity type T
        var collectionName = GetCollectionName<T>(mongodbSettings);

        // Get or create the MongoDB collection for the specified entity type T
        _collection = database.GetCollection<T>(collectionName);
    }

    // Method to determine the collection name based on the entity type T
    static string GetCollectionName<T>(IOptions<MongoDbSettings> mongodbSettings)
    {
        // Use the type of T to determine the appropriate collection name
        return typeof(T) switch
        {
            Type t when t == typeof(User) => mongodbSettings.Value.UsersCollection,
            // Add more cases for other entities as needed
            _ => throw new NotSupportedException($"Unsupported entity type: {typeof(T)}"),
        };
    }

    // Method to asynchronously create a new document in the MongoDB collection
    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    // Method to asynchronously retrieve all documents from the MongoDB collection
    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_filterBuilder.Empty).ToListAsync();
    }

    // Method to asynchronously retrieve a document by its ID from the MongoDB collection
    public async Task<T> GetAsync(string id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity._id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    // Method to asynchronously remove a document by its ID from the MongoDB collection
    public async Task<bool> RemoveAsync(string id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity._id, id);
        await _collection.DeleteOneAsync(filter);
        return true;
    }

    // Method to asynchronously update a document in the MongoDB collection
    public async Task UpdateAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        // Create a filter for the existing document based on its ID
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(existingentity => existingentity._id, entity._id);

        // Replace the existing document with the updated entity
        await _collection.ReplaceOneAsync(filter, entity);
    }
}

