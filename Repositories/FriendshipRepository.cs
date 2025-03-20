
using cinemaratona.Data;
using cinemaratona.Models;

namespace cinemaratona.Repositories;

public class FriendshipRepository(CinemaratonaContext context)
{
    private readonly CinemaratonaContext _context = context;

    public List<Friendship> List()
    {
        return _context.Friendship.ToList();
    }

    public Friendship? Include(Friendship friendship)
    {
        _context.Friendship.Add(friendship);
        _context.SaveChanges();
        return friendship;
    }

    public Friendship? Find(int id)
    {
        return _context.Friendship.FirstOrDefault(u => u.Id == id);
    }

    public Friendship? Delete(int id)
    {
        var friendship = Find(id);
        if (friendship == null) return null;
        _context.Friendship.Remove(friendship);
        _context.SaveChanges();
        return friendship;
    }

    public Friendship? Update(Friendship friendship)
    {
        var friendshipToUpdate = Find(friendship.Id);
        if (friendshipToUpdate == null) return null;
        _context.Entry(friendshipToUpdate).CurrentValues.SetValues(friendship);
        _context.SaveChanges();
        return friendshipToUpdate;
    }
}