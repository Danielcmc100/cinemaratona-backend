namespace cinemaratona.Models;
public class Friendship {
    public int Id { get; set; }
    public int User1Id { get; set; }
    public int User2Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Status { get; set; }
}