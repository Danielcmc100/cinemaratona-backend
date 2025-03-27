using cinemaratona.Models;
using cinemaratona.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace cinemaratona.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <returns>A list of <see cref="UserResponse"/> objects.</returns>
    [HttpGet]
    public ActionResult<List<UserResponse>> Get()
    {   
        var users = _userService.List().Adapt<List<UserResponse>>();
        return Ok(users);
    }

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>An <see cref="ActionResult"/> containing the user if found, otherwise a NotFound result.</returns>
    [HttpGet("{id}")]
    public ActionResult<UserResponse> Get(int id)
    {   
        User? user = _userService.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user.Adapt<UserResponse>());
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>An <see cref="IActionResult"/> containing the created user and the location of the new user.</returns>
    [HttpPost]
    public ActionResult<UserResponse> Post([FromBody] User user)
    {
        User? returned = _userService.Include(user);
        if (returned == null)
        {
            return BadRequest();
        }
        return Created(returned.Adapt<UserResponse>().Id.ToString(), returned.Adapt<UserResponse>());
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <returns>An <see cref="IActionResult"/> containing the updated user if successful, otherwise a NotFound result.</returns>
    [HttpPut]
    public IActionResult Put([FromBody] User user)
    {   
        var userToUpdate = _userService.Update(user);
        if(userToUpdate != null)
        {
            return Ok(userToUpdate.Adapt<UserResponse>());
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
        User? user = _userService.Delete(id);
        
        if (user != null)
        {
            return Ok(user.Adapt<UserResponse>());
        }
        return NotFound();
    }

    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="request">The authentication request containing email and password.</param>
    /// <returns>An <see cref="IActionResult"/> containing the authenticated user if successful, otherwise an Unauthorized result.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthRequest request)
    {
        var user = _userService.Authenticate(request.Email, request.Password);
        
        if (user != null)
        {
            return Ok(user.Adapt<UserResponse>());
        }
        
        return Unauthorized("Email ou senha inválidos");
    }
}