using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class ReviewInventory(IRepository<Review> reviewRepository, IPaginatedRepository<Request> requestRepository) : IReviewInventory
{
    public bool IsReviewable(int requestId)
    {
        var sevenDaysAgo = DateTime.Now.AddDays(-7);
        
        return requestRepository
            .GetBy(request => request.Id == requestId 
                              && request.State == RequestState.Received
                              && request.ReceivedAt <= sevenDaysAgo).Count > 0;
    }

    public Review CreateReview(Review review)
    {
        return reviewRepository.Add(review);
    }
}