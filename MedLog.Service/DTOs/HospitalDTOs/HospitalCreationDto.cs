using MedLog.Service.DTOs.AddressDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.DTOs.HospitalDTOs
{
    public class HospitalCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }  = null;

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public AddressCreationDto Address { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
    }

}
