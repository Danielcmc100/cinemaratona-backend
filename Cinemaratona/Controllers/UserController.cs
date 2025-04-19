using cinemaratona.Models;
using cinemaratona.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinemaratona.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [Authorize]
    [HttpGet]
    public ActionResult<List<UserResponse>> Get()
    {   
        var users = _userService.List().Adapt<List<UserResponse>>();
        return Ok(users);
    }

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthRequest request)
    {
        var user = _userService.Authenticate(request.Email, request.Password);
        
        if (user != null)
        {
            var tokenService = HttpContext.RequestServices.GetRequiredService<TokenService>();
            var token = tokenService.GenerateToken(user);
            
            return Ok(new 
            { 
                token,
                user = user.Adapt<UserResponse>() 
            });
        }
        
        return Unauthorized("Email ou senha inválidos");
    }
}