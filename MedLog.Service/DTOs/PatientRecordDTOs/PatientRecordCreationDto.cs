
using System.ComponentModel.DataAnnotations;

namespace MedLog.Service.DTOs.PatientRecordDTOs;
#pragma warning disable
public class PatientRecordCreationDto
{
    public string Description { get; set; } // Description of the patient's condition or visit

    [Required]
    public string Diagnosis { get; set; } // Diagnosis provided by the healthcare provider

    [Required]
    public string Symptoms { get; set; } // List of symptoms reported by the patient

    public string Medications { get; set; } // List of medications prescribed to the patient

    public string Tests { get; set; } // List of tests ordered for the patient

    public string Procedures { get; set; } // List of procedures performed on the patient
}
