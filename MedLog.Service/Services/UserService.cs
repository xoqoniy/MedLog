using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace MedLog.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public UserService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserResultDto> CreateAsync(UserCreationDto user)
        {
            var mapped = mapper.Map<User>(user);
            mapped.CreatedAt = DateTime.UtcNow;
            await repository.CreateAsync(mapped);
            return mapper.Map<UserResultDto>(mapped);

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await repository.GetAsync(id);
            if (user is null)
            {
                throw new MedLogException(404, "User not found");
            }
            await repository.RemoveAsync(id);
            return true;
        }

        public async Task<List<UserResultDto>> GetAllAsync()
        {
            var users = await repository.GetAllAsync();
            return mapper.Map<List<UserResultDto>>(users);
        }

        public async Task<UserResultDto> GetAsync(string id)
        {
            var user = await repository.GetAsync(id);
            if(user is null)
            {
                throw new MedLogException(404, "User not found");
            }
            return mapper.Map<UserResultDto>(user);
        }

        public async Task<UserResultDto> UpdateAsync(UserUpdateDto dto)
        {
            var userId = dto.Id;
            var user = await repository.GetAsync(userId);
            if(user is null)
            {
                throw new MedLogException(404, "User not found");
            }
            var modifiedUser = mapper.Map(dto, user);
            modifiedUser.LastUpdatedAt = DateTime.UtcNow;
            return mapper.Map<UserResultDto>(user);
        }
    }
}
