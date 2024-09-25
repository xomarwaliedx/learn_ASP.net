using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProj.Models;
using TestProj.DTOs;

namespace TestProj.Mapping
{
    public static class Mapper
    {
        public static Game GameDtoToGame(this CreateGameDto game){
            return new Game(){
                Name = game.Name,
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };
        }

        public static GameSummaryDto GameToGameSummaryDto(this Game game){
            return new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
        }

        public static GameDetailsDto GameToGameDetailsDto(this Game game){
            return new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
        }

        public static Game UpdateGameDtoToGame(this UpdateGamedDto game,int id){
            return new Game(){
                Id = id,
                Name = game.Name,
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };
        }
    }
}