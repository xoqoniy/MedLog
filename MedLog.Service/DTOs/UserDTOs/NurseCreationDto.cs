
using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.Extentions;
using System.Text.Json.Serialization;

namespace MedLog.Service.DTOs.UserDTOs;

public class NurseCreationDto
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Birthday { get; set; }
    public Gender Gender { get; set; }
    public int BloodType { get; set; }
    public decimal Experience { get; set; }
    public AddressCreationDto Address { get; set; }
}
