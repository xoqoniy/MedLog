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
    }
}
