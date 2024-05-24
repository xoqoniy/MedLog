using System.IO;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MedLog.Domain.Entities;
using MedLog.DAL.DbContexts;
using MongoDB.Driver.Core.Configuration;
using Microsoft.Extensions.Options;
using MedLog.DAL.IRepositories;

namespace MedLog.DAL.Repositories;

public class FileRepository : IFileRepository
{
    private readonly IMongoDatabase _database;
    private readonly string _fileStorage;
    private readonly GridFSBucket fsBucket;

    public FileRepository(IOptions<MongoDbSettings> mongodbSettings, IMongoDatabase database)
    {
        this._database = database;
        _fileStorage = mongodbSettings.Value.FilesStorage;
        fsBucket = new GridFSBucket(database, new GridFSBucketOptions { BucketName = _fileStorage });
    }

    public async Task DeleteFileAsync(string id)
    {
        var objectId = new ObjectId(id);
        await fsBucket.DeleteAsync(objectId);
    }

    public async Task<FileEntity> DownloadFileAsync(string id)
    {
        ObjectId objectId = new ObjectId(id);
        var fileInfo = await fsBucket.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync();
        if (fileInfo == null)
        {
            throw new Exception("File is null or empty");
        }
        var stream = new MemoryStream();
        await fsBucket.DownloadToStreamAsync(objectId, stream);
        stream.Seek(0, SeekOrigin.Begin);
        return new FileEntity
        {
            _id = id,
            FileName = fileInfo.Filename,
            ContentType = fileInfo.Metadata.GetValue("contentType", "application/octet-stream").AsString,
            CreatedAt = fileInfo.UploadDateTime,
            Content = stream
        };
    }

    public async Task<FileEntity> UploadFileAsync(FileEntity file)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument
            {

                    { "filename" , file.FileName },
                    { "contentType", file.ContentType },
                    { "description", file.Description}
            }
        };

        var fileId = await fsBucket.UploadFromStreamAsync(file.FileName, file.Content, options);
        return new FileEntity
        {
            _id = fileId.ToString(),
            FileName = file.FileName,
            ContentType = file.ContentType,
            Description = file.Description,
            CreatedAt = DateTime.UtcNow
        };
        
    }

}