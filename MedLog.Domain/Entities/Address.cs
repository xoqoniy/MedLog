using MedLog.Domain.Common;
using MedLog.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Domain.Entities
{
    public class Address : Auditable
    {
        
        public Region Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Neighboorhood { get; set; }
        public int HouseNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
