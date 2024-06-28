namespace MedLog.Service.DTOs.DoctorDTOs;

public class DoctorDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Specialization { get; set; }
    public double OverallRating { get; set; }
    public int RatingCount { get; set; }
}
