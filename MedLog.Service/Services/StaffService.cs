using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.StaffDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;


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

        public async Task<StaffResultDto> CreateAsync(StaffCreationDto staff)
        {
            var mapped = mapper.Map<Staff>(staff);
            mapped.CreatedAt = DateTime.UtcNow;
            await repository.CreateAsync(mapped);
            return mapper.Map<StaffResultDto>(mapped);

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var staff = await repository.GetAsync(id);
            if (staff is null)
            {
                throw new MedLogException(404, "Staff is not found");
            }
            await repository.RemoveAsync(id);
            return true;
        }

        public async Task<List<StaffResultDto>> GetAllAsync()
        {
            var staff = await repository.GetAllAsync();
            return mapper.Map<List<StaffResultDto>>(staff);
        }

        public async Task<StaffResultDto> GetAsync(string id)
        {
            var staff = await repository.GetAsync(id);
            if (staff is null)
            {
                throw new MedLogException(404, "Staff could not found");
            }
            return mapper.Map<StaffResultDto>(staff);
        }

        public async Task<StaffResultDto> UpdateAsync(string id, StaffUpdateDto dto)
        {

            var staff = await repository.GetAsync(id);
            if (staff is null)
            {
                throw new MedLogException(404, "Staff could not found");
            }
            var modifiedStaff = mapper.Map(dto, staff);
            modifiedStaff.LastUpdatedAt = DateTime.UtcNow;
            await repository.UpdateAsync(modifiedStaff);
            return mapper.Map<StaffResultDto>(modifiedStaff);
        }
    }
}

