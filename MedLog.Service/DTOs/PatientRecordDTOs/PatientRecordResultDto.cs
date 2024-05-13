namespace MedLog.Service.DTOs.PatientRecordDTOs;

#pragma warning disable
public class PatientRecordResultDto
{
    public string Id { get; set; }
    public string CreatedByUserId { get; set; }
    public string PatientId { get; set; }
    public string Description { get; set; }
    public string Diagnosis { get; set; }
    public string Symptoms { get; set; }
    public string Medications { get; set; }
    public string Tests { get; set; }
    public string Procedures { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
}
