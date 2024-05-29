﻿using System.IO;
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


        public async Task<FileResultDto> UploadFileAsync(FileCreationDto fileCreationDto)
        {
            _logger.LogInformation("Starting file upload for user {UserId}", fileCreationDto.UserId);

            if (fileCreationDto.Content == null)
            {
                _logger.LogError("File stream is null for user {UserId}", fileCreationDto.UserId);
                throw new ArgumentNullException(nameof(fileCreationDto.Content), "File stream cannot be null.");
            }

            try
            {
                using var stream = new MemoryStream();
                await fileCreationDto.Content.CopyToAsync(stream);

                var fileEntity = new FileEntity
                {
                    UserId = fileCreationDto.UserId,
                    FileName = fileCreationDto.Content.FileName,
                    ContentType = fileCreationDto.Content.ContentType,
                    Content = stream, // Assuming FileEntity stores content as byte array
                    Description = fileCreationDto.Description
                };

                var result = await _fileRepository.UploadFileAsync(fileEntity);
                return mapper.Map<FileResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the file for user {UserId}", fileCreationDto.UserId);
                throw new ApplicationException("An error occurred while uploading the file.", ex);
            }
        }

        public async Task<List<FileResultDto>> GetFilesByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            try
            {
                var files = await _fileRepository.GetFilesByUserIdAsync(userId);
                return mapper.Map<List<FileResultDto>>(files);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Getting files by UserId {userId} went wrong",userId,ex.Message);
                // Log exception (e.g., using a logging framework)
                throw new ApplicationException("An error occurred while fetching files.", ex);
            }
        }

    }
}

