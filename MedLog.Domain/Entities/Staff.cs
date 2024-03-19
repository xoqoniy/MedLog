namespace MedLog.Domain.Entities;
using MedLog.Domain.Common;
using MedLog.Domain.Enums;

#pragma warning disable

public class Staff : Auditable
{
    public Hospital HospitalName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
    public StaffRole StaffRole { get; set; }
    public string Specialization { get; set; }
    public decimal Experience { get; set; }


}