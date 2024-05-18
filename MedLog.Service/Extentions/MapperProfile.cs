using AutoMapper;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.AppointmentDTOs;
using MedLog.Service.DTOs.DoctorDTOs;
using MedLog.Service.DTOs.FileDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.DTOs.PatientRecordDTOs;
using MedLog.Service.DTOs.UserDTOs;

namespace MedLog.Service.Extentions;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
        //User Mapping
		CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, DoctorDto>().ReverseMap();
        CreateMap<User, DoctorCreationDto>().ReverseMap();
        CreateMap<User, NurseCreationDto>().ReverseMap();


        //Address Mapping
        CreateMap<Address, AddressCreationDto>().ReverseMap();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();
        CreateMap<Address, AddressResultDto>().ReverseMap();
        CreateMap<AddressResultDto, AddressUpdateDto>();

        
        //Hospital Mapping
        CreateMap<HospitalResultDto, HospitalUpdateDto>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<Hospital, HospitalCreationDto>().ReverseMap();
        CreateMap<Hospital, HospitalUpdateDto>().ReverseMap();
        CreateMap<Hospital, HospitalResultDto>().ReverseMap();

        //Appointment Mapping
        CreateMap<Appointment, AppointmentCreationDto>().ReverseMap();
        CreateMap<Appointment, AppointmentUpdateDto>().ReverseMap();
        CreateMap<Appointment, AppointmentResultDto>().ReverseMap();

        //Patient Record Mapping
        CreateMap<PatientRecord, PatientRecordCreationDto>().ReverseMap();
        CreateMap<PatientRecord, PatientRecordUpdateDto>().ReverseMap();
        CreateMap<PatientRecord, PatientRecordResultDto>().ReverseMap();

        //File Mapping
        CreateMap<FileEntity, FileCreationDto>().ReverseMap();
        CreateMap<FileEntity, FileUpdateDto>().ReverseMap();
        CreateMap<FileEntity, FileResultDto>().ReverseMap();

        
    }
}
