using MicroS_Postes.Data;
using MicroS_Postes.Repositories;
using MicroS_Postes.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Steeltoe.Discovery.Client;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDiscoveryClient(builder.Configuration);
// Load configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


//starting Connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string is not found"));
});

builder.Services.AddScoped<GestionPosteInvestisseur>();
builder.Services.AddScoped<GestionPosteStartup>();
builder.Services.AddScoped<GestionLike>();

// Ajout de XGestionUser avec sa configuration HttpClient
builder.Services.AddHttpClient<XGestionUser>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44346/");
    // Ajoutez d'autres configurations HttpClient si nécessaire
});
// Configuration du deuxième HttpClient pour GestionPosteStartup
builder.Services.AddHttpClient<GestionPosteStartup>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:5000/");
    // Ajoutez d'autres configurations HttpClient si nécessaire
});
// Configuration du deuxième HttpClient pour GestionPosteInvestisseur
builder.Services.AddHttpClient<GestionPosteInvestisseur>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:5000/");
    // Ajoutez d'autres configurations HttpClient si nécessaire
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
    {
        policy.WithOrigins("http://localhost:5289", "https://localhost:44346", "https://localhost:7225/")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithHeaders(HeaderNames.ContentType);
    });
}



app.UseAuthorization();


app.MapControllers();

app.Run();
