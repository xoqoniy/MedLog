
using System.Web;

namespace MedLog.DAL.DbContexts;
#pragma warning disable 
public class MongoDbSettings
{
    public string? ConnectionStringURL { get; init; }
    public string DatabaseName { get; init; }

    public string UsersCollection { get; set; }
    public string HospitalsCollection { get; set; }
    public string AppointmentsCollection { get; set; }
    public string PatientRecordsCollection { get; set; }
    public string FilesStorage {  get; set; }
}
