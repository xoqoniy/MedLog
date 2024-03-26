

using AutoMapper;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AddressDTOs;
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
    }
}
