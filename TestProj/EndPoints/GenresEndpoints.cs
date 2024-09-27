using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Crud_App_3.Data;
using TestProj.DTOs;

namespace TestProj.EndPoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("genres").WithParameterValidation();
            group.MapGet("", async (AppDbContext dbContext, IMapper mapper) =>
            {
                return await dbContext.Genre
                    .Select(g => mapper.Map<GenreDto>(g))
                    .ToListAsync();
            });
            return group;
        }
    }
}