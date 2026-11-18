using StoreProject.Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();