namespace cinemaratona.Models;
public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Bio { get; set; }
    public string[]? FavoriteGenres { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Links { get; set; }
    public string? PhotoUrl { get; set; }
    public int[]? FavoriteMovies { get; set; }   
}