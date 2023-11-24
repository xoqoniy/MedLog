using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;

namespace MedLog.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
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

        public async Task<UserResultDto> UpdateAsync(string id, UserUpdateDto dto)
        {
            var user = await repository.GetAsync(id);

            if (user is null)
            {
                throw new MedLogException(404, "User not found");
            }

            // Map the properties from the update DTO to the existing user
            mapper.Map(dto, user);

            // Update additional properties if needed
            user.LastUpdatedAt = DateTime.UtcNow;

            // Perform server-side validation if needed

            // Update the user
            await repository.UpdateAsync(user);

            // Return the updated user DTO
            return mapper.Map<UserResultDto>(user);
        }
    }
}
