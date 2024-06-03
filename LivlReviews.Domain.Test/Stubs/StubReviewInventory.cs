using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Clock;

namespace LivlReviews.Domain.Test.Stubs;

public class StubReviewInventory(Request request, IClock clock) : IReviewInventory
{
    public bool IsReviewable(int requestId)
    {
        return request.Id == requestId && request.State == RequestState.Received && request.ReceivedAt <= clock.Now.AddDays(-7);
    }

    public Review CreateReview(Review review)
    {
        return review;
    }
}