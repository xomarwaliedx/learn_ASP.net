using Crud_App_3.Data;

namespace TestProj.Data
{
    public static class DataExtensions
    {
        public static async Task MigrateDBAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}