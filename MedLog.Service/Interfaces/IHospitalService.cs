
using MedLog.Service.DTOs.HospitalDTOs;

namespace MedLog.Service.Interfaces;

public interface IHospitalService
{
    Task<HospitalResultDto> CreateAsync(HospitalCreationDto dto);
    Task AddUserIdToHospital(string hospitalId, string newUserId);
    Task<HospitalResultDto> UpdateAsync(string id, HospitalUpdateDto dto);
    Task<bool> DeleteByIdAsync (string id);
    Task<HospitalResultDto> GetByIdAsync (string id);
    Task<List<string>> GetHospitalsInCity(string city);
    Task<List<HospitalResultDto>> GetAllAsync();
    


}
