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