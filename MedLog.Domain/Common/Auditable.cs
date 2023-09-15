﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Common;

public abstract class Auditable
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

}
