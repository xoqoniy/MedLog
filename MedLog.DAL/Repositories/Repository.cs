using MongoDB.Driver;
using MedLog.Domain.Common;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.DAL.DbContexts;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace MedLog.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    // MongoDB collection for the specified entity type T
    private readonly IMongoCollection<T> _collection;
    private readonly IConfiguration configuration;

    // Filter builder for creating MongoDB filter definitions
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

    // Constructor that takes MongoDB settings as a dependency
    public Repository(IOptions<MongoDbSettings> mongodbSettings, IConfiguration configuration)
    {
        this.configuration = configuration;
       
        string connectionString = configuration["MongoDbSettings:ConnectionStringURL"];

        // Create a MongoDB client using the connection string
        MongoClient client = new (connectionString);

        // Get the MongoDB database using the database name from settings
        IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);

        // Determine the collection name based on the entity type T
        var collectionName = GetCollectionName<T>(mongodbSettings);

        // Get or create the MongoDB collection for the specified entity type T
        _collection = database.GetCollection<T>(collectionName);
    }

    // Method to determine the collection name based on the entity type T
    static string GetCollectionName<TEntity>(IOptions<MongoDbSettings> mongodbSettings)
    {
        // Use the type of T to determine the appropriate collection name
        return typeof(T) switch
        {
            Type t when t == typeof(User) => mongodbSettings.Value.UsersCollection,
            Type t when t == typeof(Hospital) => mongodbSettings.Value.HospitalsCollection,
            Type t when t == typeof(Appointment) => mongodbSettings.Value.AppointmentsCollection,
            Type t when t == typeof(PatientRecord) => mongodbSettings.Value.PatientRecordsCollection,
            // Add more cases for other entities as needed
            _ => throw new NotSupportedException($"Unsupported entity type: {typeof(T)}"),
        };
    }

    // Method to asynchronously create a new document in the MongoDB collection
    public async Task<T> InsertAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    // Method to asynchronously retrieve all documents from the MongoDB collection
    public async Task<List<T>> RetrieveAllAsync()
    {
        return await _collection.Find(_filterBuilder.Empty).ToListAsync();
    }

    // Method to asynchronously retrieve a document by its ID from the MongoDB collection
    public async Task<T> RetrieveByIdAsync(string id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity._id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    // Method to asynchronously remove a document by its ID from the MongoDB collection
    public async Task<bool> RemoveByIdAsync(string id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity._id, id);
        await _collection.DeleteOneAsync(filter);
        return true;
    }

    // Method to asynchronously update a document in the MongoDB collection
    public async Task ReplaceByIdAsync(T entity)
    {
        // Create a filter for the existing document based on its ID
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(existingentity => existingentity._id, entity._id);

        // Replace the existing document with the updated entity
        await _collection.ReplaceOneAsync(filter, entity);
    }

    // Method to asynchronously perform a partial update on a document in the MongoDB collection
    public async Task PartialUpdateAsync(string id, UpdateDefinition<T> updateDefinition)
    {
        // Create a filter for the document based on its ID
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(entity => entity._id, id);

        // Perform the partial update using the provided update definition
        await _collection.UpdateOneAsync(filter, updateDefinition);

    }
    //Method to retrieve entities based on a provided expression
    public async Task<List<T>> RetrieveByExpressionAsync(Expression<Func<T, bool>> expression)
    {
        return await _collection.Find(expression).ToListAsync();
    }

}

