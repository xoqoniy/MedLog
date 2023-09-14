

namespace MedLog.Domain.Common;

public abstract class Auditable
{
    []
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

}
