using cinemaratona.Models;
using cinemaratona.Repositories;

namespace cinemaratona.Services;

public class ReviewService(ReviewRepository reviewRepository)
{
    private readonly ReviewRepository _reviewRepository = reviewRepository;

    public List<Review> List()
    {
        return _reviewRepository.List();
    }

    public Review? Include(Review review_obj)
    {
        return _reviewRepository.Include(review_obj);
    }

    public Review? Find(int id)
    {
        return _reviewRepository.Find(id);
    }

    public Review? Delete(int id)
    {
        return _reviewRepository.Delete(id);
    }

    public Review? Update(Review review_obj)
    {
        var existingReview = _reviewRepository.Find(review_obj.Id);
        if (existingReview == null) return null;
        
        return _reviewRepository.Update(review_obj);
    }
}