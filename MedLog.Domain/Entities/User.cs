
using MedLog.Domain.Common;
using MedLog.Domain.Enums;
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
        public string Address { get; set; }
        public Region Region { get; set; }
        public string City { get; set; }
    }
}
