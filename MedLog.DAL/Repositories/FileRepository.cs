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

namespace MedLog.DAL.Repositories
{
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
            await fsBucket.DeleteAsync(ObjectId.Parse(id));
        }

        public async Task<Stream> DownloadFileAsync(string id)
        {
            return await fsBucket.OpenDownloadStreamAsync(ObjectId.Parse(id));
        }

        
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var id = await fsBucket.UploadFromStreamAsync(fileName, fileStream);
            return id.ToString();
        }

    }
}
