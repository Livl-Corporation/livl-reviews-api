using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Clock;

namespace LivlReviews.Domain.Test.Stubs;

public class FakeReviewInventory(Request request, IClock clock) : IReviewInventory
{
    public bool IsReviewableDateReached(int requestId)
    {
        return request.Id == requestId && clock.Now >= request.ReviewableAt;
    }

    public Review CreateReview(Review review)
    {
        return review;
    }

    public bool HasStatusReceived(int requestId)
    {
        return request.Id == requestId && request.State == RequestState.Received;
    }
}