#pragma warning disable

using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MedLog.Service.DTOs.UserDTOs;

public class UserCreationDto
{
    [Required(ErrorMessage = "Ismni to'ldirish kerak")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Familiyani to'ldirish kerak")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Telefon raqamni to'ldirish kerak")]

    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public int BloodType { get; set; }
    public Address Address { get; set; }

}
