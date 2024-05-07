using MedLog.Domain.Enums;
using MedLog.Service.DTOs.AddressDTOs;

namespace MedLog.Service.DTOs.UserDTOs;

public class DoctorCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public int BloodType { get; set; }
    public AddressCreationDto Address { get; set; }
    public Role UserRole { get; set; } = Role.Doctor;
    public string Specialization { get; set; }
    public decimal Experience { get; set; }
}
