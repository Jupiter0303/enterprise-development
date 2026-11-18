var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Museum_Api>("api");

builder.Build().Run();