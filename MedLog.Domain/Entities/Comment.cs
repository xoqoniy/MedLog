﻿
using MongoDB.Bson.Serialization.Attributes;

namespace MedLog.Domain.Entities;

public class Comment
{
    public string DoctorId { get; set; }
    public string CommentatorId { get; set; }
    public string Content { get; set; }
    [BsonIgnoreIfNull]
    public List<Comment> Replies { get; set; } = new List<Comment>();
}