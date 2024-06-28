

using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;
using System.ComponentModel.DataAnnotations;

#pragma warning disable
namespace MedLog.Service.DTOs.UserDTOs;

public class UserUpdateDto
{
    
    [Required(ErrorMessage = "Ismni to'ldirish kerak")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Telefon raqamni to'ldirish kerak")]

    public string PhoneNumber { get; set; }
    public string Birthday { get; set; }
    public Gender Gender { get; set; }
    public int BloodType { get; set; }
    public AddressUpdateDto Address {  get; set; }
}
