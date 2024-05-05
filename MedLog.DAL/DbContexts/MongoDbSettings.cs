
using System.Web;

namespace MedLog.DAL.DbContexts;

public class MongoDbSettings
{
    public string? ConnectionStringURL { get; init; }
    public string DatabaseName { get; init; }

    public string UsersCollection { get; set; }
    public string HospitalsCollection { get; set; }
    public string AppointmentsCollection { get; set; }
}
