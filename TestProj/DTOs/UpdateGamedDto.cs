using System.ComponentModel.DataAnnotations;

namespace TestProj.DTOs;

public record class UpdateGamedDto(
    [Required][StringLength(50)] string Name,
    int GenreId,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate
);