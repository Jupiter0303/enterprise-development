using Museum.Application.Analytics;
using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Visitors;
using Museum.Application.Services;
using Museum.Domain;
using Museum.Domain.Data;
using Museum.Infrastructure.InMemory;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var seed = DataSeeder.Create();

builder.Services.AddSingleton<IRepository<Visitor>>(new InMemoryRepository<Visitor>(seed.Visitors));
builder.Services.AddSingleton<IRepository<Ticket>>(new InMemoryRepository<Ticket>(seed.Tickets));
builder.Services.AddSingleton<IRepository<Exhibition>>(new InMemoryRepository<Exhibition>(seed.Exhibitions));
builder.Services.AddSingleton<IRepository<Excursion>>(new InMemoryRepository<Excursion>(seed.Excursions));
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddScoped<IVisitorService, VisitorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();