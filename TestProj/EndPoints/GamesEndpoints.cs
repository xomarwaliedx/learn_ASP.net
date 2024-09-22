using System;
using TestProj.DTOs;

namespace TestProj.EndPoints;

public static class GamesEndpoints
{
    const string GetGameEndpontName = "GetGame";

    readonly static List<GameDto> games = new()
    {
        new GameDto(1, "Halo", "Shooter", 59.99m, new DateOnly(2001, 11, 15)),
        new GameDto(2, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
        new GameDto(3, "The Witcher 3: Wild Hunt", "Action role-playing", 39.99m, new DateOnly(2015, 5, 19))
    };

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) => games.Find(g => g.Id == id) is GameDto game ? Results.Ok(game) : Results.NotFound()).WithName(GetGameEndpontName);

        group.MapPost("", (CreateGameDto game) =>
        {
            var newGame = new GameDto(games.Count + 1, game.Name, game.Genre, game.Price, game.ReleaseDate);
            games.Add(newGame);
            return Results.CreatedAtRoute(GetGameEndpontName, new { id = newGame.Id }, newGame);
        });

        group.MapPut("/{id}", (int id, UpdateGamedDto game) =>
        {
            if (games.Find(g => g.Id == id) is not GameDto existingGame)
            {
                return Results.NotFound();
            }

            var updatedGame = new GameDto(id, game.Name, game.Genre, game.Price, game.ReleaseDate);
            games[games.IndexOf(existingGame)] = updatedGame;
            return Results.Ok(updatedGame);
        });

        group.MapDelete("/games/{id}", (int id) =>
        {
            if (games.Find(g => g.Id == id) is not GameDto existingGame)
            {
                return Results.NotFound();
            }

            games.Remove(existingGame);
            return Results.NoContent();
        });

        return group;
    }
}
