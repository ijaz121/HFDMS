using CommonServices.Claims;
using CommonServices.EncyptionDecryption;
using CommonServices.ListConverter;
using CommonServices.Middleware;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.ActivityLog;
using Repository.Dashboard;
using Repository.HealthFacility;
using Repository.HealthWorker;
using Repository.Login;
using Repository.Patient;
using Repository.RoleManagement;
using Repository.UserManagement;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"], // Fetch from appsettings.json
            ValidAudience = configuration["JwtSettings:Audience"], // Fetch from appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])) // Fetch the secret key from appsettings.json
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HFDMS API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});


builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDashboardRepo, DashboardRepo>();
builder.Services.AddScoped<IHealthFacilityRepo, HealthFacilityRepo>();
builder.Services.AddScoped<IHealthWorkerRepo, HealthWorkerRepo>();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddSingleton<IListConverter, ListConverter>();
builder.Services.AddSingleton<IDbConnectionLogic, DbConnectionLogic>();
builder.Services.AddScoped<IRoleManagementRepo, RoleManagementRepo>();
builder.Services.AddScoped<IUserManagementRepo, UserManagementRepo>();
builder.Services.AddScoped<IActivityLogRepo, ActivityLogRepo>();
builder.Services.AddScoped<IEncyptDecryptService, EncyptDecryptService>();
builder.Services.AddScoped<IClaimValues, ClaimValues>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseMiddleware<ActivityLogMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.MapControllers();
app.Run();
