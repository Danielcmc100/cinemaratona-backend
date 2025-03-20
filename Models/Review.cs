namespace cinemaratona.Models;

public class Review {    
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string? Opinion { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Rating { get; set; }
    public bool Recommended { get; set; }
    public bool Watched { get; set; }
}