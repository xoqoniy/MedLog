using MedLog.Domain.Common;
using MedLog.Domain.Enums;

#pragma warning disable

namespace MedLog.Domain.Entities;

public class Hospital : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Owner {get; set; }
    public string Address { get; set; }
    public Region Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int ZipCode { get; set; }
}
