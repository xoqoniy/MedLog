namespace MedLog.Service.DTOs.PatientRecordDTOs;

public class PatientRecordUpdateDto
{
    public string Description { get; set; } // Description of the patient's condition or visit

    public string Diagnosis { get; set; } // Diagnosis provided by the healthcare provider

    public string Symptoms { get; set; } // List of symptoms reported by the patient

    public string Medications { get; set; } // List of medications prescribed to the patient

    public string Tests { get; set; } // List of tests ordered for the patient

    public string Procedures { get; set; } // List of procedures performed on the patient
}
