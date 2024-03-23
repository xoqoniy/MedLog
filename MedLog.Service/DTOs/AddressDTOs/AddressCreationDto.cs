using MedLog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.DTOs.AddressDTOs
{
    public class AddressCreationDto
    {
        public string? _id { get; set; }
        public Region Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Neighboorhood { get; set; }
        public int HouseNumber { get; set; }
    }
}
