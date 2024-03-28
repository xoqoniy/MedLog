
using MedLog.Service.DTOs.HospitalDTOs;

namespace MedLog.Service.Interfaces;

public interface IHospitalService
{
    Task<HospitalResultDto> CreateAsync(HospitalCreationDto dto);
    Task<HospitalResultDto> UpdateAsync(string id, HospitalUpdateDto dto);
    Task<HospitalResultDto> GetByIdAsync (string id);
    Task<bool> DeleteByIdAsync (string id);
    Task<List<HospitalResultDto>> GetAllAsync();
    Task<List<HospitalResultDto>> GetHospitalsInCity(string city);


}
