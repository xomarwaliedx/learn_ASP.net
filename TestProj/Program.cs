using Crud_App_3.Data;
using TestProj.Data;
using TestProj.DTOs;
using TestProj.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString=builder.Configuration.GetConnectionString("MySqlConn");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGet("/", () => "Hello World!");

await  app.MigrateDBAsync();

app.Run();
