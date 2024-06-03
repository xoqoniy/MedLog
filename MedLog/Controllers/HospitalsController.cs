using MedLog.Domain.Configurations;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedLog.Controllers
{
    public class HospitalsController : RestFulSense
    {
        private readonly IHospitalService service;

        public HospitalsController(IHospitalService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<HospitalResultDto>> PostAsync (HospitalCreationDto dto)
        {
            var hospital = await service.CreateAsync(dto);
            return hospital;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalResultDto>> GetByIdAsync (string id)
        {
            var hospital = await service.GetByIdAsync(id);
            return hospital;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteByIdAsync(string id)
        {
            var hospital = await service.DeleteByIdAsync(id);
            return true;
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult<HospitalResultDto>> UpdateByIdAsync([FromRoute] string id, [FromBody] HospitalUpdateDto dto)
        {
            var hospital = await service.UpdateAsync(id, dto);
            return hospital;
        }

        //Get/hospitals
        [HttpGet]
        public async Task<ActionResult<PaginationResult<HospitalResultDto>>> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var result = await service.GetAllHospitalsAsync(@params);
            return Ok(result);
        }


        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<HospitalResultDto>>> GetHospitalsInCity(string city)
        {
            var hospitals =  await service.GetHospitalsInCity(city);
            return Ok(hospitals);
        }

    }
}
