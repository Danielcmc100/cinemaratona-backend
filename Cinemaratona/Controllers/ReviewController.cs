using cinemaratona.Models;
using cinemaratona.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinemaratona.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(ReviewService reviewService) : ControllerBase
{
    private readonly ReviewService _reviewService = reviewService;

    [HttpGet]
    public ActionResult<List<Review>> Get()
    {   
        var reviews = _reviewService.List().Adapt<List<Review>>();
        return Ok(reviews);
    }

    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {   
        Review? review = _reviewService.Find(id);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review.Adapt<Review>());
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Review> Post([FromBody] Review review)
    {
        Review? returned = _reviewService.Include(review);
        if (returned == null)
        {
            return BadRequest();
        }
        return Created(returned.Adapt<Review>().Id.ToString(), returned.Adapt<Review>());
    }

    [Authorize]
    [HttpPut]
    public IActionResult Put([FromBody] Review review)
    {   
        var reviewToUpdate = _reviewService.Update(review);
        if(reviewToUpdate != null)
        {
            return Ok(reviewToUpdate.Adapt<Review>());
        }
        return NotFound();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Review? review = _reviewService.Delete(id);
        
        if (review != null)
        {
            return Ok(review.Adapt<Review>());
        }
        return NotFound();
    }
}