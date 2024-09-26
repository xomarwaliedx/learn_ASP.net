using Crud_App_3.Data;
using TestProj.Data;
using TestProj.DTOs;
using TestProj.EndPoints;
using Microsoft.OpenApi.Models;  
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

var connectionString=builder.Configuration.GetConnectionString("MySqlConn");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Version = "v1",
        Title = "TestProj API",
        Description = "An API for managing games and genres"
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestProj API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI the default page
    });
}

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await  app.MigrateDBAsync();

app.Run();
