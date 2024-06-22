using MedLog.Domain.Common;

#pragma warning disable

namespace MedLog.Domain.Entities
{
    public class StarRating : Auditable
    {
        public string DoctorId { get; set; }
        public string StarGiverId { get; set; }
        public int Star { get; set; } //From 1 to 5 
    }
}
