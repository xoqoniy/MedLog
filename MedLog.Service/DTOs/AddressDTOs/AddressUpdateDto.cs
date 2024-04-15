using MedLog.Domain.Enums;
#pragma warning disable

namespace MedLog.Service.DTOs.AddressDTOs;

public class AddressUpdateDto
{
    public Region Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Neighboorhood { get; set; }
    public int HouseNumber { get; set; }
}
