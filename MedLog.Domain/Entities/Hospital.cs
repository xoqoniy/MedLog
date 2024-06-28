using MedLog.Domain.Common;
using MedLog.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

#pragma warning disable

namespace MedLog.Domain.Entities;

public class Hospital : Auditable
{
    public string Name { get; set; }
    [BsonIgnoreIfNull]
    public string? Description { get; set; } = null;
    public string Owner {get; set; }
    public Address Address { get; set; }
    public int ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> UserIds { get; set; }
}
