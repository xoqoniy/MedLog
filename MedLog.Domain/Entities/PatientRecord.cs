using MedLog.Domain.Common;

#pragma warning disable 

namespace MedLog.Domain.Entities
{
    public class PatientRecord : Auditable
    {
        public User Patient { get; set; } // Reference to the patient (User entity)

        public List<string>? Description { get; set; } // Description of the patient's condition or visit

        public string Diagnosis { get; set; } // Diagnosis provided by the healthcare provider

        public List<string> Symptoms { get; set; } // List of symptoms reported by the patient

        public List<string>? Medications { get; set; } // List of medications prescribed to the patient

        public List<string>? Tests { get; set; } // List of tests ordered for the patient

        public List<string>? Procedures { get; set; } // List of procedures performed on the patient

    }
}
