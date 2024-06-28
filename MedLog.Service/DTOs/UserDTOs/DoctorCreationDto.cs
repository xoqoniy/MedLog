using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using MedLog.Service.Extentions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedLog.Service.DTOs.UserDTOs;

public class DoctorCreationDto
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public Gender Gender { get; set; }
    public int BloodType { get; set; }
    public AddressCreationDto Address { get; set; }
    public string Specialization { get; set; }
    public decimal Experience { get; set; }
}
