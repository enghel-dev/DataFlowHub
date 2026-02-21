using DataFlowHub.Application.Interfaces;
using DataFlowHub.Application.Services;
using DataFlowHub.Infrastructure;
using DataFlowHub.Application;
using DataFlowHub.Infrastructure.DataBase;
using DataFlowHub.Infrastructure.Repository;
using System.Data.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Database Connection
var ConectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddSingleton(new DBconnectionFactory(ConectionString!));

//Inyeccion de dependencias
// --- Registro de Capas (Limpio y Profesional) ---
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddControllers();
//


//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI( s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "DataFlowHub.API v1");
    s.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
