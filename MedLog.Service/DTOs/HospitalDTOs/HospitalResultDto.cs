using DnsClient.Protocol;
using MedLog.Service.DTOs.AddressDTOs;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable
namespace MedLog.Service.DTOs.HospitalDTOs;

public class HospitalResultDto
{
    public string? _id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Owner { get; set; }
    public AddressResultDto Address { get; set; }
    public int ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> UserIds { get; set; }
}
