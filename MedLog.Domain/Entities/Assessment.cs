
namespace MedLog.Domain.Entities;

public class Assessment
{
    public string DoctorId { get; set; }
    public int Star {  get; set; }
    public int StarCount { get; set; } //Number of people who gave stars 
    public decimal TotalStarRating { get; set; }
    public string StarGiverId { get; set; }
    public string Comment { get; set; }
    public string CommentatorId { get; set; }

}
