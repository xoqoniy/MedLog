using System.Threading.Tasks;
using MedLog.Service.DTOs.FileDTOs;

namespace MedLog.Service.IServices
{
    public interface IFileService
    {
        Task<FileResultDto> UploadFileAsync(string userId, FileCreationDto fileCreationDto);
        Task<FileResultDto> DownloadFileAsync(string id);
        Task<bool> DeleteFileAsync(string id);
        Task<List<FileResultDto>> GetFilesByUserIdAsync(string userId);

    }
}
