using MedLog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.DTOs.StaffDTOs
{
    public class StaffCreationDto
    {
        public string HospitalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public StaffRole StaffRole { get; set; }
        public string Specialization { get; set; }
        public decimal Experience { get; set; }
    }
}
