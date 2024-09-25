using Crud_App_3.Data;

namespace TestProj.Data
{
    public static class DataExtensions
    {
        public static void MigrateDB(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}