using MedLog.DAL.IRepositories;
using MedLog.DAL.Repositories;
using MedLog.Service.Interfaces;
using MedLog.Service.Services;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace MedLog.Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHospitalService, HospitalService>();
    }
}
