var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");

var db = mysql.AddDatabase("museam");

builder.AddProject<Projects.Museum_Api>("api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();