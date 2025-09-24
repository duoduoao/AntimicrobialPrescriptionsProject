using AntimicrobialPrescriptions.Application.Interfaces;
using AntimicrobialPrescriptions.Application.Services;
using AntimicrobialPrescriptions.Domain.Interfaces;
using AntimicrobialPrescriptions.Infrastructure.Data;
using AntimicrobialPrescriptions.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
 

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

 
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

// Add services to the container.
// Add CORS setup here
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular dev server origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

 builder.Services.AddControllers();
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Clinician", policy =>
        policy.RequireRole("Clinician"));
    options.AddPolicy("InfectionControl", policy =>
        policy.RequireRole("InfectionControl"));
});

 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
 

app.UseRouting();
// Enable CORS for Angular frontend before authentication/authorization
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();
