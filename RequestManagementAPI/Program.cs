using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestManagementAPI.Controllers;
using RequestManagementAPI.Models;
using RequestManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your DbContext (replace YourConnectionString with your actual connection string)
builder.Services.AddDbContext<DbContextAssesment>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("assesment")));

// Register your service as Scoped
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<LoginService>();

// *** CORS Configuration ***
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost7102", builder => // Named policy
    {
        builder.WithOrigins("https://localhost:7102")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // If using credentials
    });
    // You can add more policies here if needed
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// *** IMPORTANT: CORS middleware goes *before* routing and authorization ***
app.UseCors("AllowLocalhost7102"); // Use your named policy

app.MapControllers();

app.Run();
