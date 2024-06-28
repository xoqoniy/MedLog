using AutoMapper;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.AppointmentDTOs;
using MedLog.Service.DTOs.DoctorDTOs;
using MedLog.Service.DTOs.FileDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.DTOs.PatientRecordDTOs;
using MedLog.Service.DTOs.UserDTOs;
using System.Globalization;

namespace MedLog.Service.Extentions;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
        //User Mapping

        CreateMap<UserCreationDto, User>()
           .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));
        CreateMap<User, UserCreationDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Birthday.Day))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Birthday.Month))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Birthday.Year));
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, DoctorDto>().ReverseMap();
        CreateMap<DoctorCreationDto, User>()
                 .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));
        CreateMap<User, DoctorCreationDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Birthday.Day))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Birthday.Month))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Birthday.Year));
        CreateMap<NurseCreationDto, User>()
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));
        CreateMap<User, NurseCreationDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Birthday.Day))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Birthday.Month))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Birthday.Year));


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
        CreateMap<FileEntity, FileCreationDto>()
     .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content)); // Assuming the property names are the same
        CreateMap<FileCreationDto, FileEntity>().ReverseMap();

        CreateMap<FileEntity, FileResultDto>().ReverseMap();

        
    }
}
