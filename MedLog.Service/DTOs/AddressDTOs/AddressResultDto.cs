
using MedLog.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Service.DTOs.AddressDTOs;

public class AddressResultDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public Region Region { get; set; }
    public string City { get; set; }
    public string Neighboorhood { get; set; }
    public string Street { get; set; }  
    public int HouseNumber {  get; set; }
}
