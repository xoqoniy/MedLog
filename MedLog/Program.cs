using AutoMapper;
using MedLog.DAL.DbContexts;
using MedLog.DAL.Repositories;
using MedLog.Domain.Entities;
using MedLog.Extensions;
using MedLog.Service.Extentions;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Microsoft.OpenApi.Models;
using MedLog.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
internal class Program
{
    private static void Main(string[] args)
    {
        // Configure Serilog at the very beginning
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
            .Enrich.FromLogContext()
            .CreateLogger();
        try
        {
            Log.Information("Starting up the application");
            var builder = WebApplication.CreateBuilder(args);
            //Configuration of User secrets for database connection string
            builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

            builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Registering Services
            builder.Services.AddCustomServices();
            // Register MongoDB settings
            builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

            // Register MongoDB client and database
            builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionStringURL);
            });

            builder.Services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return client.GetDatabase(settings.DatabaseName);
            });

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Adding Serilog - Logging 
            builder.Logging.AddSerilog(Log.Logger);


            //Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            //Mapping
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            //Convert api url name to dash case
            builder.Services.AddControllers(options =>
                options.Conventions.Add(
                    new RouteTokenTransformerConvention(new RouteConfiguration())));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
        catch(Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
}