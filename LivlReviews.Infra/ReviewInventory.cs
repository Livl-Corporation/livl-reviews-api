using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class ReviewInventory(IRepository<Review> reviewRepository, IPaginatedRepository<Request> requestRepository) : IReviewInventory
{
    public bool IsReviewableDateReached(int requestId)
    {
        return requestRepository
            .GetBy(request => request.Id == requestId 
                              && DateTime.Now >= request.ReviewableAt).Count > 0;
    }
    
    public bool HasStatusReceived(int requestId)
    {
        return requestRepository
            .GetBy(request => request.Id == requestId 
                              && request.State == RequestState.Received).Count > 0;
    }

    public Review CreateReview(Review review)
    {
        return reviewRepository.Add(review);
    }
}