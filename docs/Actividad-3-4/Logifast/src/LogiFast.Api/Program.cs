using LogiFast.Application.Ports;
using LogiFast.Application.UseCases;
using LogiFast.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IDeliveryRepository, InMemoryDeliveryRepository>();
builder.Services.AddScoped<AssignCourierUseCase>();

var app = builder.Build();

app.MapControllers();

app.Run();