using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.StaffDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.Services
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<Staff> repository;
        private readonly IMapper mapper;

        public StaffService(IRepository<Staff> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<StaffResultDto> CreateAsync(StaffCreationDto user)
        {
            var mapped = mapper.Map<Staff>(user);
            mapped.CreatedAt = DateTime.UtcNow;
            await repository.CreateAsync(mapped);
            return mapper.Map<StaffResultDto>(mapped);

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

        public async Task<List<StaffResultDto>> GetAllAsync()
        {
            var users = await repository.GetAllAsync();
            return mapper.Map<List<StaffResultDto>>(users);
        }

        public async Task<StaffResultDto> GetAsync(string id)
        {
            var user = await repository.GetAsync(id);
            if (user is null)
            {
                throw new MedLogException(404, "User not found");
            }
            return mapper.Map<StaffResultDto>(user);
        }

        public async Task<StaffResultDto> UpdateAsync(string id, StaffUpdateDto dto)
        {

            var user = await repository.GetAsync(id);
            if (user is null)
            {
                throw new MedLogException(404, "User not found");
            }
            var modifiedUser = mapper.Map(dto, user);
            modifiedUser.LastUpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(modifiedUser);
            return mapper.Map<StaffResultDto>(user);
        }
    }
}

