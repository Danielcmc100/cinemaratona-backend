
using cinemaratona.Models;
using cinemaratona.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinemaratona.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService): ControllerBase
{
    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="User"/> objects.</returns>
    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return Ok(userService.List());
    }

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the user if found, otherwise a NotFound result.</returns>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {   
        User? user = userService.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>An <see cref="IActionResult"/> containing the created user and the location of the new user.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        User? returned = userService.Include(user);
        if (returned == null)
        {
            return BadRequest();
        }
        return CreatedAtAction("Get", new { id = returned.Id }, returned);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <returns>An <see cref="IActionResult"/> containing the updated user if successful, otherwise a NotFound result.</returns>
    [HttpPut]
    public IActionResult Put([FromBody] User user)
    {   
        var userToUpdate = userService.Update(user);
        if(userToUpdate != null)
        {
            return Ok(user);
        }
        return NotFound();
    }

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>An <see cref="IActionResult"/> containing the deleted user if successful, otherwise a NotFound result.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        User? user = userService.Delete(id);
        
        if (user != null)
        {
            return Ok(user);
        }
        return NotFound();
    }
}