

using AutoMapper;
using MedLog.Domain.Entities;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MongoDB.Bson;

namespace MedLog.Service.Extentions;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();

        CreateMap<Address, AddressCreationDto>().ReverseMap();
        CreateMap<Address, AddressResultDto>().ReverseMap();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();


        CreateMap<ObjectId, string>().ConvertUsing(id => id.ToString());
        CreateMap<string, ObjectId>().ConvertUsing(id => ObjectId.Parse(id));
    }
}
