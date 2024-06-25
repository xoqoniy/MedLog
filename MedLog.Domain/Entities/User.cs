
using MedLog.Domain.Common;
using MedLog.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Net;
using System.Net.Cache;

#pragma warning disable

namespace MedLog.Domain.Entities
{
    public class User : Auditable
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public Gender Gender { get; set; }
        public int BloodType { get; set; }
        public Address Address { get; set; }
        public Role UserRole { get; set; } = Role.User;

        // Additional fields for staff members
        [BsonIgnoreIfNull]
        public string Specialization { get; set; }
        [BsonIgnoreIfNull]
        public decimal Experience { get; set; }

        // HospitalId to associate user with hospital
        public string HospitalId { get; set; }

        //Rating for Doctors
        [BsonIgnoreIfDefault]
        public int OverallRating { get; set; } //On a scale of 1 to 5
        [BsonIgnoreIfNull]
        public int RatingCount { get; set; } //Count of people who rated 

    }
}
 