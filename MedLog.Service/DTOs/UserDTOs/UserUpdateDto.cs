

using MedLog.Domain.Entities;
using MedLog.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

#pragma warning disable
namespace MedLog.Service.DTOs.UserDTOs;

public class UserUpdateDto
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
}
