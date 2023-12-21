using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Domain.Common
{
    public interface IEntityWithObjectId
    {
        ObjectId FileId { get; set; }
    }
}
