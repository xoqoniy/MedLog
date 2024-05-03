using MedLog.Service.DTOs.AddressDTOs;
using System.Text.Json.Serialization;
#pragma warning disable 

namespace MedLog.Service.DTOs.HospitalDTOs;

public class HospitalUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Owner { get; set; }
    public AddressUpdateDto Address { get; set; }
    public int ZipCode { get; set; }
    public string PhoneNumber { get; set; }


}
