using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vetsys.API.Data;
using Vetsys.API.Modules.Customers;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;
using Vetsys.API.Modules.Customers.Services;
using Vetsys.API.Modules.Customers.UseCases.CreateCustomer;
using Vetsys.API.Modules.Customers.UseCases.DeleteCustomer;
using Vetsys.API.Modules.Customers.Validators;
using Vetsys.API.Modules.Notifications.Contracts;
using Vetsys.API.Modules.Notifications.Services;    
using Vetsys.API.Modules.Notifications.UseCases.SendExpiratedVaccineEmail;
using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.Pets.Services;
using Vetsys.API.Modules.Pets.UseCases.CreatePet;
using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.Services;
using Vetsys.API.Modules.VaccinationRecords.UseCases.CreateVaccinationRecord;
using Vetsys.API.Modules.VaccinationRecords.UseCases.FindPendingVaccination;
using Vetsys.API.Modules.VaccinationRecords.Workers;
using Vetsys.API.Modules.VaccinesTypes.Contracts;
using Vetsys.API.Modules.VaccinesTypes.Services;
using Vetsys.API.Modules.VaccinesTypes.UseCases.CreateVaccineType;
using Vetsys.API.Shared.Filters;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// Configurar el logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VetsysDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IVaccineTypeRepository, VaccineTypeRepository>();
builder.Services.AddScoped<IVaccinationRecordRepository, VaccinationRecordRepository>();
builder.Services.AddScoped<CreatePetUseCase>();
builder.Services.AddScoped<CreateCustomerUseCase>();
builder.Services.AddScoped<DeleteCustomerUseCase>();
builder.Services.AddScoped<CreateVaccineTypeUseCase>();
builder.Services.AddScoped<CreateVaccinationRecordUseCase>();
builder.Services.AddScoped<FindPendingVaccinationUseCase>();  
builder.Services.AddScoped<SendExpiratedVaccineEmailUseCase>();
builder.Services.AddScoped<IEmailSender, EmailSender>();



// Registrar el validador de FluentValidation
builder.Services.AddScoped<IValidator<CustomerCreateDTO>, CustomerCreateDTOValidator>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailOnVaccinationExpiredEvent>();
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddHostedService<NotifyPendingVaccionationWorker>();
builder.Services.AddScoped<ValidationFilter>();


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
