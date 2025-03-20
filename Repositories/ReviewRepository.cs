
using cinemaratona.Data;
using cinemaratona.Models;

namespace cinemaratona.Repositories;

public class ReviewRepository(CinemaratonaContext context)
{
    private readonly CinemaratonaContext _context = context;

    public List<Review> List()
    {
        return _context.Review.ToList();
    }

    public Review? Include(Review review)
    {
        _context.Review.Add(review);
        _context.SaveChanges();
        return review;
    }

    public Review? Find(int id)
    {
        return _context.Review.FirstOrDefault(u => u.Id == id);
    }

    public Review? Delete(int id)
    {
        var review = Find(id);
        if (review == null) return null;
        _context.Review.Remove(review);
        _context.SaveChanges();
        return review;
    }

    public Review? Update(Review review)
    {
        var reviewToUpdate = Find(review.Id);
        if (reviewToUpdate == null) return null;
        _context.Entry(reviewToUpdate).CurrentValues.SetValues(review);
        _context.SaveChanges();
        return reviewToUpdate;
    }
}