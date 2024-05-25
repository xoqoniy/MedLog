using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.DAL.Repositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.FileDTOs;
using MedLog.Service.IServices;
using Microsoft.Extensions.Logging;

namespace MedLog.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<FileService> _logger;
        private readonly IMapper mapper;

        public FileService(IFileRepository fileRepository, ILogger<FileService> logger, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _logger = logger;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteFileAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("File ID cannot be null or empty.", nameof(id));

            try
            {
                await _fileRepository.DeleteFileAsync(id);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while deleting FileId -> {id} ", id);
                throw new ApplicationException("An error occurred while deleting the file.", ex);
            }
        }

        public async Task<FileResultDto> DownloadFileAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("File ID cannot be null or empty.", nameof(id));

            try
            {
                var result = await _fileRepository.DownloadFileAsync(id);
                return mapper.Map<FileResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApplicationException("An error occurred while downloading the file.", ex);
            }
        }


        public async Task<FileResultDto> UploadFileAsync(string userId, FileCreationDto fileCreationDto)
        {
            _logger.LogInformation("Starting file upload for user {UserId}", userId);

            if (fileCreationDto.Content == null)
            {
                _logger.LogError("File stream is null for user {UserId}", userId);
                throw new ArgumentNullException(nameof(fileCreationDto.Content), "File stream cannot be null.");
            }

            // Check if the stream position is not at the beginning, then set it to the beginning
            if (fileCreationDto.Content.Position != 0)
            {
                fileCreationDto.Content.Seek(0, SeekOrigin.Begin);
            }

            if (string.IsNullOrWhiteSpace(fileCreationDto.FileName))
            {
                _logger.LogError("File name is null or empty for user {UserId}", userId);
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileCreationDto.FileName));
            }

            if (string.IsNullOrWhiteSpace(fileCreationDto.ContentType))
            {
                _logger.LogError("Content type is null or empty for user {UserId}", userId);
                throw new ArgumentException("Content type cannot be null or empty.", nameof(fileCreationDto.ContentType));
            }

            try
            {
                var file = mapper.Map<FileEntity>(fileCreationDto);
                file.UserId = userId;
                var result = await _fileRepository.UploadFileAsync(file);
                return mapper.Map<FileResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the file for user {UserId}", userId);
                throw new ApplicationException("An error occurred while uploading the file.", ex);
            }
        }
    }
}

