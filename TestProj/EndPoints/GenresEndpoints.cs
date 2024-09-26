using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_App_3.Data;
using TestProj.Mapping;

namespace TestProj.EndPoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("genres").WithParameterValidation();
            group.MapGet("", async (AppDbContext dbContext) =>
            {
                return await dbContext.Genre
                    .Select(g => g.GenreToGenreDto())
                    .ToListAsync();
            });
            return group;
        }
    }
}