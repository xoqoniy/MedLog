using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.DTOs.PatientRecordDTOs
{
    public class PatientRecordResultDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string StaffId { get; set; }
        public string FileId { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string Medications { get; set; }
        public string Procedures { get; set; }
        public string Allergies { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
