using cinemaratona.Data;
using cinemaratona.Models;

namespace cinemaratona.Repositories;

public class EventRepository(CinemaratonaContext context)
{
    private readonly CinemaratonaContext _context = context;

    public List<Event> List()
    {
        return _context.Event.ToList();
    }

    public Event? Include(Event event_obj)
    {
        try
        {
            _context.Event.Add(event_obj);
            _context.SaveChanges();
            return event_obj;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public Event? Find(int id)
    {
        return _context.Event.FirstOrDefault(u => u.Id == id);
    }

    public Event? Delete(int id)
    {
        var event_obj = Find(id);
        if (event_obj == null) return null;
        _context.Event.Remove(event_obj);
        _context.SaveChanges();
        return event_obj;
    }

    public Event? Update(Event event_obj)
    {
        var event_objToUpdate = Find(event_obj.Id);
        if (event_objToUpdate == null) return null;
        _context.Entry(event_objToUpdate).CurrentValues.SetValues(event_obj);
        _context.SaveChanges();
        return event_objToUpdate;
    }
}