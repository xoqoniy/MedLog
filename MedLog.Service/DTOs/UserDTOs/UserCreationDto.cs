#pragma warning disable
using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedLog.Service.DTOs.UserDTOs;

public class UserCreationDto
{
    [Required(ErrorMessage = "Ismni to'ldirish kerak")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Telefon raqamni to'ldirish kerak")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Day is required")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31")]
    public int Day { get; set; }

    [Required(ErrorMessage = "Month is required")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
    public int Month { get; set; }

    [Required(ErrorMessage = "Year is required")]
    [Range(1900, 2100, ErrorMessage = "Year must be a valid year")]
    public int Year { get; set; }
    public Gender Gender { get; set; }
    public int BloodType { get; set; }
    public AddressCreationDto Address { get; set; }
    [JsonIgnore]
    public DateTime Birthday => new DateTime(Year, Month, Day, 0, 0, 0, DateTimeKind.Utc);

}
