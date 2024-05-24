using MedLog.Domain.Entities;
using System.IO;
using System.Threading.Tasks;

namespace MedLog.DAL.IRepositories
{
    public interface IFileRepository
    {
        Task<FileEntity> UploadFileAsync(FileEntity file);
        Task<FileEntity> DownloadFileAsync(string id);
        Task DeleteFileAsync(string id);
    }
}
