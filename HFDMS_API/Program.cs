using CommonServices.ListConverter;
using DAL;
using Repository.ActivityLog;
using Repository.Dashboard;
using Repository.HealthFacility;
using Repository.HealthWorker;
using Repository.Login;
using Repository.Patient;
using Repository.RoleManagement;
using Repository.UserManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseMiddleware<ActivityLogMiddleware>();
app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
