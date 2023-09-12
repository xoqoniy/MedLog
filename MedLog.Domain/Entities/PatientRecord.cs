using MedLog.Domain.Common;

namespace MedLog.Domain.Entities;

public class PatientRecord : Auditable
{
    public Staff? Staff { get; set; }
    public User? User { get; set; }
    public string File { get; set; }
    public string Description { get; set; }

}
