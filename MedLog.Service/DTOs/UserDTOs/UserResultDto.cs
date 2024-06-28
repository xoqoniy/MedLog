using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.DTOs.HospitalDTOs;
using MedLog.Service.Extentions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


#pragma warning disable
namespace MedLog.Service.DTOs.UserDTOs
{
    public class UserResultDto
    { 
        public string _id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public int BloodType { get; set; }
        public AddressResultDto Address { get; set; }
        public string HospitalId {  get; set; }
    }

}
