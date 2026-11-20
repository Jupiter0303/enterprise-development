using Microsoft.EntityFrameworkCore;
using Museum.Api;
using Museum.Application.Analytics;
using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;
using Museum.Application.Services;
using Museum.Infrastructure.EfCore;
using Museum.Infrastructure.InMemory;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddExceptionHandler<RelatedEntityExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var provider = builder.Configuration["Storage:Provider"] ?? "EfCore";

if (provider == "InMemory")
    builder.Services.AddInMemoryRepositories();
else
{
    builder.AddMySqlDbContext<MuseumDbContext>("museum");
    builder.Services.AddEfCoreRepositories();
}

builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<IExcursionService, ExcursionService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();
app.MapDefaultEndpoints();

if (provider != "InMemory")
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<MuseumDbContext>().Database.Migrate();
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();