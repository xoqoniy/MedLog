using MedLog.Service.DTOs.AppointmentDTOs;
using MedLog.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedLog.Controllers
{
    public class AppointmentsController : RestFulSense
    {
        private readonly IAppointmentService service;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            this.service = appointmentService;
        }

        [HttpPost("appointments/{patientId}/{doctorId}")]
        public async Task<ActionResult<AppointmentResultDto>> CreateAppointmentAsync(AppointmentCreationDto dto, [FromRoute]string patientId, [FromRoute]string doctorId)
        {
            var appointment = await service.CreateAsync(dto, patientId, doctorId);
            return Ok(appointment);
        }

        [HttpGet("patientId/{patientId}")]
        public async Task<ActionResult<List<AppointmentResultDto>>> GetAppointmentsByPatientId(string patientId)
        {
            var appointment =  await service.GetAppointmentsByPatientIdAsync(patientId);
            return Ok(appointment);
        }

        [HttpGet("doctorId/{doctorId}")]
        public async Task<ActionResult<List<AppointmentResultDto>>> GetAppointmentsByDoctorId(string doctorId)
        {
            var appointment = await service.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(appointment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentResultDto>> UpdateAppointmentById(AppointmentUpdateDto dto, string patientId)
        {
            var appointment = await service.UpdateAsync(dto, patientId);
            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAppointmentById(string appointmentId)
        {
            var appointment = await service.DeleteByAppointmentIdAsync(appointmentId);
            return Ok(appointment);
        } 


    }
}
