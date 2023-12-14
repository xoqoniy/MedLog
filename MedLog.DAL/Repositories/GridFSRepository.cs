using MedLog.DAL.DbContexts;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Common;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Runtime.CompilerServices;

namespace MedLog.DAL.Repositories;

public class GridFSRepository<T> : IGridFSRepository<T> where T : Auditable, IEntityWithObjectId
{
    private readonly IMongoDatabase mongodb;
    private readonly IGridFSBucket gridFSBucket;

    public GridFSRepository(IOptions<MongoDbSettings> mongodbSettings) 
    {
        MongoClient client = new (mongodbSettings.Value.ConnectionStringURL);
        mongodb = client.GetDatabase(mongodbSettings.Value.DatabaseName);

        // Setup GridFS bucket with custom options
        var options = new GridFSBucketOptions
        {
            BucketName = "PatientRecord", // Custom bucket name
            ChunkSizeBytes = 1024 // Custom chunk size
        };
        gridFSBucket = new GridFSBucket(mongodb, options);
    }

    public async Task<T> CreateAsync(T entity, Stream fileStream, string fileName)
    {
        var fileId = await UploadFileAsync(fileStream, fileName);
        entity.FileId = fileId;

        await mongodb.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
        return entity;
    }

    public async Task<Stream> DownloadFileAsync(string fileId)
    {
        var objectId = new ObjectId(fileId);
        return await gridFSBucket.OpenDownloadStreamAsync(objectId);
    }

    public async Task<ObjectId> UploadFileAsync(Stream fileStream, string fileName)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument("contentType", "application/octet-stream")
        };

        return await gridFSBucket.UploadFromStreamAsync(fileName, fileStream, options);
    }

    public async Task<T> GetAsync(ObjectId id)
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
        return await mongodb.GetCollection<T>(typeof(T).Name).Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> RemoveAsync(ObjectId id)
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
        DeleteResult result = await mongodb.GetCollection<T>(typeof(T).Name).DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        // Create a filter for the existing document based on its ID
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(existingentity => existingentity.Id, entity.Id);

        // Replace the existing document with the updated entity
        await mongodb.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(filter, entity);
    }
}
   

   
   

