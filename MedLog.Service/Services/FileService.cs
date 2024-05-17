using System.IO;
using System.Threading.Tasks;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.FileDTOs;
using MedLog.Service.IServices;

namespace MedLog.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileResultDto> UploadFileAsync(FileCreationDto fileCreationDto)
        {
            // Upload file to GridFS
            var fileId = await _fileRepository.UploadFileAsync(fileCreationDto.FileStream, fileCreationDto.FileName);

            // Create FileEntity
            var fileEntity = new FileEntity
            {
                _id = fileId,
                UserId = fileCreationDto.UserId,
                FileName = fileCreationDto.FileName,
                ContentType = fileCreationDto.ContentType,
                Length = fileCreationDto.FileStream.Length,
                CreatedAt = DateTime.UtcNow,
            };

            // Convert to FileResultDto
            var fileResultDto = new FileResultDto
            {
                Id = fileEntity._id,
                UserId = fileEntity.UserId,
                FileName = fileEntity.FileName,
                ContentType = fileEntity.ContentType,
                Length = fileEntity.Length,
                CreatedAt = fileEntity.CreatedAt,
            };

            return fileResultDto;
        }

        public async Task<FileResultDto> DownloadFileAsync(string id)
        {
            var fileStream = await _fileRepository.DownloadFileAsync(id);

            // Assuming you have a way to retrieve file details from the database, you can do so here
            // For now, we'll mock the details
            var fileDetails = new FileEntity
            {
                _id = id,
                FileName = "mockFileName",
                ContentType = "application/pdf",
                Length = fileStream.Length,
                CreatedAt = DateTime.UtcNow,
            };

            // Convert to FileResultDto
            var fileResultDto = new FileResultDto
            {
                Id = fileDetails._id,
                UserId = fileDetails.UserId,
                FileName = fileDetails.FileName,
                ContentType = fileDetails.ContentType,
                Length = fileDetails.Length,
                CreatedAt = fileDetails.CreatedAt,
            };

            return fileResultDto;
        }

        public async Task<bool> DeleteFileAsync(string id)
        {
            await _fileRepository.DeleteFileAsync(id);
            return true;
        }

        public async Task<FileResultDto> UpdateFileAsync(FileUpdateDto fileUpdateDto)
        {
            // Assuming you have a way to retrieve and update file details in the database, you can do so here
            // For now, we'll mock the update
            var fileDetails = new FileEntity
            {
                _id = fileUpdateDto.Id,
                FileName = fileUpdateDto.FileName,
                ContentType = fileUpdateDto.ContentType,
                UserId = fileUpdateDto.UserId,
            };

            // Convert to FileResultDto
            var fileResultDto = new FileResultDto
            {
                Id = fileDetails._id,
                UserId = fileDetails.UserId,
                FileName = fileDetails.FileName,
                ContentType = fileDetails.ContentType,
                Length = fileDetails.Length,
                CreatedAt = fileDetails.CreatedAt,
            };

            return fileResultDto;
        }
    }
}
