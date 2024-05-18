using System.Threading.Tasks;
using MedLog.Service.DTOs.FileDTOs;

namespace MedLog.Service.IServices
{
    public interface IFileService
    {
        Task<FileResultDto> UploadFileAsync(FileCreationDto fileCreationDto);
        Task<FileResultDto> DownloadFileAsync(string id);
        Task<bool> DeleteFileAsync(string id);
        FileResultDto UpdateFile(FileUpdateDto fileUpdateDto);
    }
}
