
using MedLog.Domain.Common;
using MongoDB.Bson;

namespace MedLog.DAL.IRepositories
{
    public interface IGridFSRepository<T> where T : Auditable, IEntityWithObjectId
    {
        Task<T> CreateAsync(T entity, Stream fileStream, string fileName);
        Task<ObjectId> UploadFileAsync(Stream fileStream, string fileName);
        Task<Stream> DownloadFileAsync(string fileId);
    }
}
