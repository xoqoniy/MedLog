using MedLog.Service.DTOs.AppointmentDTOs;
using MedLog.Service.DTOs.PatientRecordDTOs;
using MedLog.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedLog.Controllers;


public class PatientRecordsController : RestFulSense
{
    private readonly IPatientRecordService service;
    public PatientRecordsController(IPatientRecordService service)
    {
        this.service = service;
    }

    [HttpPost("patient-record")]
    public async Task<ActionResult<PatientRecordResultDto>> CreatePatientRecordAsync(string patientId, PatientRecordCreationDto dto)
    {
        var patientRecord = await service.CreateAsync(patientId, dto);
        return Ok(patientRecord);
    }

    [HttpGet("id")]
    public async Task<ActionResult<IEnumerable<PatientRecordResultDto>>> GetPatientRecordById(string patientId)
    {
        var record = await service.GetPatientRecordsById(patientId);
        return Ok(record);  
    }
    //GetAll patientRecords
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientRecordResultDto>>> GetPatientRecords()
    {
        var records = await service.GetAllPatientRecords();
        return Ok(records);
    }

    [HttpPut("id")]
    public async Task<ActionResult<PatientRecordResultDto>> UpdateAppointmentByRecordId(string patientRecordId, PatientRecordUpdateDto dto)
    {
        var record = await service.UpdateAsync(patientRecordId,dto);
        return Ok(record);
    }

    [HttpDelete("id")]
    public async Task<ActionResult<bool>> DeleteAppointmentById(string patientRecordId)
    {
        bool record = await service.DeleteAsync(patientRecordId) ;
        return Ok(record);
    }

    [HttpGet("patientrecord/{recordId}")]
    public async Task<ActionResult<PatientRecordResultDto>> GetPatientRecordByRecordId(string recordId)
    {
        var record = await service.GetPatientRecordById(recordId) ;
        return Ok(record);
    }

}
