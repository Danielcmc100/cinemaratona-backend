
using cinemaratona.Data;
using cinemaratona.Models;

namespace cinemaratona.Repositories;

public class UserRepository(CinemaratonaContext context)
{
    private readonly CinemaratonaContext _context = context;

    public List<User> List()
    {
        return _context.User.ToList();
    }

    public User? Include(User user)
    {
        _context.User.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? Find(int id)
    {
        return _context.User.FirstOrDefault(u => u.Id == id);
    }

    public User? Delete(int id)
    {
        var user = Find(id);
        if (user == null) return null;
        _context.User.Remove(user);
        _context.SaveChanges();
        return user;
    }

    public User? Update(User user)
    {
        var userToUpdate = Find(user.Id);
        if (userToUpdate == null) return null;
        _context.Entry(userToUpdate).CurrentValues.SetValues(user);
        _context.SaveChanges();
        return userToUpdate;
    }
}