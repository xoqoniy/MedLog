using MedLog.DAL.IRepositories;
using MedLog.DAL.Repositories;
using MedLog.Service.Extentions;
using MedLog.Service.Interfaces;
using MedLog.Service.IServices;
using MedLog.Service.Services;
using Microsoft.AspNetCore.DataProtection.Repositories;
using MongoDB.Driver;

namespace MedLog.Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFileRepository, FileRepository>();


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHospitalService, HospitalService>();
        services.AddScoped<IAppointmentService,  AppointmentService>();
        services.AddScoped<IPatientRecordService, PatientRecordService>();
        services.AddScoped<IFileService,  FileService>();

        
    }
}
