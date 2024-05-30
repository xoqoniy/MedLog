using AutoMapper;
using MedLog.DAL.IRepositories;
using MedLog.DAL.Repositories;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AppointmentDTOs;
using MedLog.Service.Exceptions;
using MedLog.Service.Interfaces;

namespace MedLog.Service.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IRepository<Appointment> appointmentRepository;
    private readonly IUserService userService;

    private readonly IMapper mapper;

    public AppointmentService(IRepository<Appointment> repository, IUserService userService, IMapper mapper)
    {
        this.userService = userService;
        this.appointmentRepository = repository;
        this.mapper = mapper;
    }

    public async Task<AppointmentResultDto> CreateAsync(AppointmentCreationDto dto, string patientId, string doctorId)
    {
        // Validate appointmentDto and patientId
        if (string.IsNullOrEmpty(patientId))
            throw new MedLogException(400, "Patient ID is required");

        var patient = await userService.GetAsync(patientId);

        if (patient == null)
            throw new MedLogException(404, "Patient not found");

        string hospitalId = patient.HospitalId;

        bool isDoctorAvailable = await userService.IsDoctorAvailableAtTimeAsync(dto.AppointmentDateTime, doctorId);
        if (!isDoctorAvailable)
            throw new MedLogException(400, "Chosen doctor is not available at the selected time");

        // Map the appointmentDto to Appointment entity
        var newAppointment = mapper.Map<Appointment>(dto);

        // Assign patient and doctor to the appointment
        newAppointment.PatientId = patientId;
        newAppointment.DoctorId = doctorId;

        // Save the appointment to the database
        await appointmentRepository.InsertAsync(newAppointment);

        // Map the created appointment to AppointmentResultDto
        return mapper.Map<AppointmentResultDto>(newAppointment);

    }

    public async Task<bool> DeleteByAppointmentIdAsync(string appointmentId)
    {
        var appointment = await appointmentRepository.RetrieveByIdAsync(appointmentId);
        if (appointment == null)
            throw new MedLogException(404, "Appointment not found");

        await appointmentRepository.RemoveByIdAsync(appointmentId);
        return true;
    }

    public async Task<List<AppointmentResultDto>> GetAllAppointmentsAsync()
    {
        var appointments = await appointmentRepository.RetrieveAllAsync();
        return mapper.Map<List<AppointmentResultDto>>(appointments);
    }

    public async Task<List<AppointmentResultDto>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {

        var appointments = await appointmentRepository.RetrieveByExpressionAsync(appointment => appointment.DoctorId == doctorId);
        return mapper.Map<List<AppointmentResultDto>>(appointments);
    }

    public async Task<List<AppointmentResultDto>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        var appointments = await appointmentRepository.RetrieveByExpressionAsync(appointment => appointment.PatientId == patientId);
        return mapper.Map<List<AppointmentResultDto>>(appointments);
    }

    public async Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto, string appointmentId)
    {
        var appointment = await appointmentRepository.RetrieveByIdAsync(appointmentId);
        if (appointment == null)
            throw new MedLogException(404, "Appointment not found");

        mapper.Map(dto, appointment);

        // Perform any additional validation or business logic

        await appointmentRepository.ReplaceByIdAsync(appointment);

        return mapper.Map<AppointmentResultDto>(appointment);
    }
}

    




