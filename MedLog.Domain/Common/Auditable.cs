using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;

namespace MedLog.Domain.Common;

public abstract class Auditable
{
    [Key]
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    public ObjectId _id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }

}
