using cinemaratona.Models;
using cinemaratona.Repositories;

namespace cinemaratona.Services;

public class EventService(EventRepository eventRepository)
{
    private readonly EventRepository _eventRepository = eventRepository;

    public List<Event> List()
    {
        return _eventRepository.List();
    }

    public Event? Include(Event event_obj)
    {
        return _eventRepository.Include(event_obj);
    }

    public Event? Find(int id)
    {
        return _eventRepository.Find(id);
    }

    public Event? Delete(int id)
    {
        return _eventRepository.Delete(id);
    }

    public Event? Update(Event event_obj)
    {
        var existingEvent = _eventRepository.Find(event_obj.Id);
        if (existingEvent == null) return null;
        
        return _eventRepository.Update(event_obj);
    }
}