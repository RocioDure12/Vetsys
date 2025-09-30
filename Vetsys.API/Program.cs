using Microsoft.EntityFrameworkCore;
using Vetsys.API.Data;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.Services;
using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.Pets.Services;
using Vetsys.API.Modules.Customers.UseCases.CreateCustomer;
using Vetsys.API.Modules.Customers.UseCases.DeleteCustomer;
using Vetsys.API.Modules.Pets.UseCases.CreatePet;
using Vetsys.API.Modules.VaccinesTypes.Contracts;
using Vetsys.API.Modules.VaccinesTypes.Services;
using Vetsys.API.Modules.VaccinesTypes.UseCases.CreateVaccineType;
using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.UseCases.CreateVaccinationRecord;
using Vetsys.API.Modules.VaccinationRecords.Services;
using Vetsys.API.Modules.VaccinationRecords.UseCases.NotifyPendingVaccination;
using MassTransit;
using Vetsys.API.Modules.Notifications.Contracts;
using Vetsys.API.Modules.Notifications.Services;    
using Vetsys.API.Modules.Notifications.UseCases.SendExpiratedVaccineEmail;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddScoped<NotifyPendingVaccinationUseCase>();  
builder.Services.AddScoped<SendExpiratedVaccineEmailUseCase>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailOnVaccinationExpiredEvent>();
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddHostedService<Vetsys.API.Modules.VaccinationRecords.Workers.NotifyPendingVaccionationWorker>();

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
