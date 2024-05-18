using System.IO;
using System.Threading.Tasks;

namespace MedLog.DAL.IRepositories
{
    public interface IFileRepository
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task<Stream> DownloadFileAsync(string id);
        Task DeleteFileAsync(string id);
    }
}
