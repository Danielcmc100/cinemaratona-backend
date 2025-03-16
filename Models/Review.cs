namespace cinemaratona.Models;

public class Review {    public int Id { get; set; }
    public int Userid { get; set; }
    public int Movieid { get; set; }
    public string? Opinion { get; set; }
    public DateTime Createdat { get; set; }
    public int Rating { get; set; }
    public bool Recommended { get; set; }
    public bool Watched { get; set; }
}