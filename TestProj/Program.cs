using TestProj.DTOs;
using TestProj.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();
