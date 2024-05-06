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

    public async Task<AppointmentResultDto> CreateAsync(AppointmentCreationDto dto, string patientId)
    {
        try
        {
            // Validate appointmentDto and patientId
            if (string.IsNullOrEmpty(patientId))
                throw new MedLogException(400, "Patient ID is required");
            
            var patient = await userService.GetAsync(patientId);

            if (patient == null)
                throw new MedLogException(404, "Patient not found");

            string hospitalId = patient.HospitalId;

            var doctors = await userService.GetDoctorsByHospitalId(hospitalId);
            var chosenDoctor = doctors.FirstOrDefault(d => d.Id == dto.DoctorId);
            if (chosenDoctor == null)
                throw new MedLogException(400, "Chosen doctor is not available at the selected time");

            bool isDoctorAvailable = await userService.IsDoctorAvailableAtTimeAsync(dto.AppointmentDateTime, chosenDoctor.Id);
            if (!isDoctorAvailable)
                throw new MedLogException(400, "Chosen doctor is not available at the selected time");


            // Map the appointmentDto to Appointment entity
            var newAppointment = mapper.Map<Appointment>(dto);

            // Assign patient and doctor to the appointment
            newAppointment.PatientId = patientId;
            newAppointment.DoctorId = chosenDoctor.Id;

            // Save the appointment to the database
            await appointmentRepository.InsertAsync(newAppointment);

            // Map the created appointment to AppointmentResultDto
            return mapper.Map<AppointmentResultDto>(newAppointment);

        }
        catch(Exception ex)
        {
            throw new MedLogException(403, $"{ex.Message}");
        }
    }

    public Task<bool> DeleteAsync(string appointmentId)
    {
        throw new NotImplementedException();
    }

    public Task<List<AppointmentResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentResultDto> GetByPatientIdAsync(string patientId)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto, string patientId)
    {
        throw new NotImplementedException();
    }
}
