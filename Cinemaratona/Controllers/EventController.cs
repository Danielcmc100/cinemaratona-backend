using cinemaratona.Models;
using cinemaratona.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinemaratona.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(EventService eventService) : ControllerBase
{
    private readonly EventService _eventService = eventService;

    [Authorize]
    [HttpGet]
    public ActionResult<List<Event>> Get()
    {   
        var events = _eventService.List().Adapt<List<Event>>();
        return Ok(events);
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<Event> Get(int id)
    {   
        Event? event_obj = _eventService.Find(id);
        if (event_obj == null)
        {
            return NotFound();
        }
        return Ok(event_obj.Adapt<Event>());
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Event> Post([FromBody] Event event_obj)
    {
        Event? returned = _eventService.Include(event_obj);
        if (returned == null)
        {
            return BadRequest();
        }
        return Created(returned.Adapt<Event>().Id.ToString(), returned.Adapt<Event>());
    }

    [Authorize]
    [HttpPut]
    public IActionResult Put([FromBody] Event event_obj)
    {   
        var eventToUpdate = _eventService.Update(event_obj);
        if(eventToUpdate != null)
        {
            return Ok(eventToUpdate.Adapt<Event>());
        }
        return NotFound();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Event? event_obj = _eventService.Delete(id);
        
        if (event_obj != null)
        {
            return Ok(event_obj.Adapt<Event>());
        }
        return NotFound();
    }
}