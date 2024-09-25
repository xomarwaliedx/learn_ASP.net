using TestProj.Models;

namespace Crud_App_3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Game> Game { get; set; }

        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Adventure"
                },
                new Genre
                {
                    Id = 3,
                    Name = "RPG"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Simulation"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Strategy"
                }
            );
            
    }

    }
    
}