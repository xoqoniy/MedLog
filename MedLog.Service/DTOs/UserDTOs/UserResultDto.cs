using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


#pragma warning disable
namespace MedLog.Service.DTOs.UserDTOs
{
    public class UserResultDto
    { 
        public ObjectId _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int BloodType { get; set; }
        public AddressResultDto Address { get; set; }

    }

}
