using cinemaratona.Models;
using cinemaratona.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace cinemaratona.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FriendshipController(FriendshipService friendshipService) : ControllerBase
{
    private readonly FriendshipService _friendshipService = friendshipService;

    [HttpGet]
    public ActionResult<List<Friendship>> Get()
    {   
        var friendships = _friendshipService.List().Adapt<List<Friendship>>();
        return Ok(friendships);
    }

    [HttpGet("{id}")]
    public ActionResult<Friendship> Get(int id)
    {   
        Friendship? friendship = _friendshipService.Find(id);
        if (friendship == null)
        {
            return NotFound();
        }
        return Ok(friendship.Adapt<Friendship>());
    }

    [HttpPost]
    public ActionResult<Friendship> Post([FromBody] Friendship friendship)
    {
        Friendship? returned = _friendshipService.Include(friendship);
        if (returned == null)
        {
            return BadRequest();
        }
        return Created(returned.Adapt<Friendship>().Id.ToString(), returned.Adapt<Friendship>());
    }

    [HttpPut]
    public IActionResult Put([FromBody] Friendship friendship)
    {   
        var friendshipToUpdate = _friendshipService.Update(friendship);
        if(friendshipToUpdate != null)
        {
            return Ok(friendshipToUpdate.Adapt<Friendship>());
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Friendship? friendship = _friendshipService.Delete(id);
        
        if (friendship != null)
        {
            return Ok(friendship.Adapt<Friendship>());
        }
        return NotFound();
    }
}