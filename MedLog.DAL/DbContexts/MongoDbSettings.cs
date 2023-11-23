﻿
namespace MedLog.DAL.DbContexts;

public class MongoDbSettings
{
    public string? ConnectionStringURL { get; init; }
    public string DatabaseName { get; init; }

    public string UsersCollection { get; set; }
    public string StaffCollection { get; set; }
}
