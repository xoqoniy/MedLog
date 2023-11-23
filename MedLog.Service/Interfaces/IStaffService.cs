using MedLog.Service.DTOs.StaffDTOs;
using MedLog.Service.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.Interfaces
{
    public interface IStaffService
    {
        Task<StaffResultDto> CreateAsync(StaffCreationDto user);
        Task<StaffResultDto> UpdateAsync(string id, StaffUpdateDto dto);
        Task<bool> DeleteAsync(string id);
        Task<StaffResultDto> GetAsync(string id);
        Task<List<StaffResultDto>> GetAllAsync();
    }
}
