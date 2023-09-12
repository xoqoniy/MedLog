using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.UserDTOs;
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

        public Task<UserResultDto> CreateAsync(UserCreationDto user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserResultDto?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResultDto> UpdateAsync(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }
    }
}
