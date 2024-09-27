using System;
using AutoMapper;
using Crud_App_3.Data;
using TestProj.DTOs;
using TestProj.Models;
using Microsoft.Extensions.Logging; // Ensure to include this namespace

namespace TestProj.EndPoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";


    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", async (AppDbContext dbContext, IMapper mapper) =>
{
            var games = await dbContext.Game
                .Include(game => game.Genre)
                .AsNoTracking()
                .ToListAsync();

        //     foreach (var game in games)
        // {
        //     logger.LogInformation("Game ID: {Id}, Name: {Name}, Genre: {Genre}, Price: {Price}, Release Date: {ReleaseDate}",
        //         game.Id, game.Name, game.Genre?.Name ?? "No Genre", game.Price, game.ReleaseDate);
        // }


            var gameSummaryDtos = mapper.Map<List<GameSummaryDto>>(games);

            return Results.Ok(gameSummaryDtos);
        });

        group.MapGet("/{id}", async (int id, AppDbContext dbContext, IMapper mapper) =>
        {
            Game? game = await dbContext.Game.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(mapper.Map<GameDetailsDto>(game));
        }).WithName(GetGameEndpointName);

        group.MapPost("", async (CreateGameDto game, AppDbContext dbContext, IMapper mapper) =>
        {
            Game newGame = mapper.Map<Game>(game);
            newGame.Genre = await dbContext.Genre.FindAsync(game.GenreId);
            dbContext.Game.Add(newGame);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, mapper.Map<GameSummaryDto>(newGame));
        });

        group.MapPut("/{id}", async (int id, UpdateGamedDto game, AppDbContext dbContext, IMapper mapper) =>
        {
            var existingGame = await dbContext.Game.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame).CurrentValues.SetValues(mapper.Map<Game>(game, opts => opts.Items["Id"] = id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, AppDbContext dbContext) =>
        {
            await dbContext.Game.Where(g => g.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
