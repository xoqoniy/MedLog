using AutoMapper;
using MedLog.DAL.DbContexts;
using MedLog.DAL.Repositories;
using MedLog.Domain.Entities;
using MedLog.Extensions;
using MedLog.Service.Extentions;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Configuration of User secrets for database connection string
        builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

        //MongoDb Database Configuration
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //Registering Services
        builder.Services.AddCustomServices();

        builder.Services.AddAutoMapper(typeof(MapperProfile));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}