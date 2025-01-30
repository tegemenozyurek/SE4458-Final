using Microsoft.EntityFrameworkCore;
using Prescription_and_Doctor_Visit_Management_System.Data;
using Prescription_and_Doctor_Visit_Management_System.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure circular reference handling
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MedicineService to the DI container (for MongoDB operations)
builder.Services.AddSingleton<MedicineService>();

// Add RabbitMqService to DI container for messaging
builder.Services.AddSingleton<RabbitMqService>();

// Configure CORS to allow requests from frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Log connection string in development mode for debugging
    Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");
}

// Use CORS middleware
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
