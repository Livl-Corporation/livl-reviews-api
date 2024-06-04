using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class ReviewInventory(IRepository<Review> reviewRepository, IPaginatedRepository<Request> requestRepository) : IReviewInventory
{
    public Review CreateReview(Review review)
    {
        return reviewRepository.Add(review);
    }
}