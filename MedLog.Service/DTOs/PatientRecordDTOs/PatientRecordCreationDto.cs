﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLog.Service.DTOs.PatientRecordDTOs
{
    public class PatientRecordCreationDto
    {
        public string UserId { get; set; }
        public string StaffId { get; set; }
        public IFormFile File { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string Medications { get; set; }
        public string Procedures { get; set; }
        public string Allergies { get; set; }
        public string Description { get; set; }
    }
}