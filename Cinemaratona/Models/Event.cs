namespace cinemaratona.Models;
public class Event {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int[] UsersId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public required string Location { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; }
}