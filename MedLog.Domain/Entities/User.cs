
using MedLog.Domain.Common;
using MedLog.Domain.Enums;
using System.Data;
using System.Net;
using System.Net.Cache;

#pragma warning disable

namespace MedLog.Domain.Entities
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int BloodType { get; set; }
        public Address Address { get; set; }
        public Role UserRole { get; set; } = Role.User;

        // Additional fields for staff members
        public string Specialization { get; set; }
        public decimal Experience { get; set; }

        // HospitalId to associate user with hospital
        public string HospitalId { get; set; }
    }
}
 