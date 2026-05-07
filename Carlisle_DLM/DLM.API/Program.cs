
using DLM.API.Middleware;
using DLM.Application.Common.Automapper;
using DLM.Application.Features.Doctors.Commands;
using DLM.Application.Features.Doctors.Queries;
using DLM.Application.Features.Doctors.Validators;
using DLM.Application.Interfaces.Doctors;
using DLM.Infrastructure.Persistence;
using DLM.Infrastructure.Repositories.Doctors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);



//  SERILOG COnfigr
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        shared: true)
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ---------------- DB CONTEXT ----------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        });
});


// ---------------- fluent validation ----------------
builder.Services.AddValidatorsFromAssembly(
    typeof(CreateDoctorCommandValidator).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));


// ---------------- MediatR for CQRS (Command and qeuries) ----------------
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(CreateDoctorCommand).Assembly
      
    ));

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));


// ---------------- Di----------------
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();


// ---------------- Automapper ----------------
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();