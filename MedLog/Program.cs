using AutoMapper;
using MedLog.DAL.DbContexts;
using MedLog.DAL.Repositories;
using MedLog.Domain.Entities;
using MedLog.Extensions;
using MedLog.Service.Extentions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

//MongoDb Database Configuration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Add services to the container.



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
