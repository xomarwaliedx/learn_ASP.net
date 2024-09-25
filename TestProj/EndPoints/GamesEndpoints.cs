using System;
using Crud_App_3.Data;
using TestProj.DTOs;
using TestProj.Mapping;
using TestProj.Models;

namespace TestProj.EndPoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";


    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", async (AppDbContext dbContext) => Results.Ok(
            await dbContext.Game.Include(game=>game.Genre).Select(g => g.GameToGameSummaryDto()).AsNoTracking().ToListAsync()));

        group.MapGet("/{id}", async (int id, AppDbContext dbContext) => {
        Game? game = await dbContext.Game.FindAsync(id);
        return game is null?Results.NotFound():Results.Ok(game.GameToGameDetailsDto());
        }).WithName(GetGameEndpointName);

        group.MapPost("", async (CreateGameDto game, AppDbContext dbContext) =>
        {
            Game newGame=game.GameDtoToGame();
            newGame.Genre=await dbContext.Genre.FindAsync(game.GenreId);
            dbContext.Game.Add(newGame);
            await dbContext.SaveChangesAsync();
            
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, newGame.GameToGameSummaryDto());
        });

        group.MapPut("/{id}", async (int id, UpdateGamedDto game,AppDbContext dbContext) =>
        {
            var existingGame = await dbContext.Game.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame).CurrentValues.SetValues(game.UpdateGameDtoToGame(existingGame.Id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id,AppDbContext dbContext) =>
        {
            await dbContext.Game.Where(g=>g.Id==id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
