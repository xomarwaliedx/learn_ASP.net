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

        group.MapGet("/", (AppDbContext dbContext) => Results.Ok(
            dbContext.Game.Include(game=>game.Genre).Select(g => g.GameToGameSummaryDto()).AsNoTracking()));

        group.MapGet("/{id}", (int id, AppDbContext dbContext) => {
        Game? game= dbContext.Game.Find(id);
        return game is null?Results.NotFound():Results.Ok(game.GameToGameDetailsDto());
        }).WithName(GetGameEndpointName);

        group.MapPost("", (CreateGameDto game, AppDbContext dbContext) =>
        {
            Game newGame=game.GameDtoToGame();
            newGame.Genre=dbContext.Genre.Find(game.GenreId);
            dbContext.Game.Add(newGame);
            dbContext.SaveChanges();
            
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, newGame.GameToGameSummaryDto());
        });

        group.MapPut("/{id}", (int id, UpdateGamedDto game,AppDbContext dbContext) =>
        {
            var existingGame = dbContext.Game.Find(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame).CurrentValues.SetValues(game.UpdateGameDtoToGame(existingGame.Id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id,AppDbContext dbContext) =>
        {
            dbContext.Game.Where(g=>g.Id==id).ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
