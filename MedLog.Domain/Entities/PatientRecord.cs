using MedLog.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace MedLog.Domain.Entities;

public class PatientRecord : Auditable
{
    public Staff? Staff { get; set; }
    public User? User { get; set; }
    public IFormFile File { get; set; }
    public string Diagnosis { get; set; }
    public string Symptoms { get; set; }
    public string Medications { get; set; }
    public string Procedures {  get; set; }
    public string Allergies { get; set; }
    public string Description { get; set; }

}
