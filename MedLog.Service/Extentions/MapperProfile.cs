

using AutoMapper;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.UserDTOs;

namespace MedLog.Service.Extentions;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
    }
}
