

using AutoMapper;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.DTOs.UserDTOs;

namespace MedLog.Service.Extentions;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();

        CreateMap<Address, AddressCreationDto>().ReverseMap();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();
        CreateMap<Address, AddressResultDto>().ReverseMap();


        CreateMap<HospitalResultDto, HospitalUpdateDto>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));


        CreateMap<AddressResultDto, AddressUpdateDto>();
        CreateMap<Hospital, HospitalCreationDto>().ReverseMap();
        CreateMap<Hospital, HospitalUpdateDto>().ReverseMap();
        CreateMap<Hospital, HospitalResultDto>().ReverseMap();
    }
}
