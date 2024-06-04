using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IReviewManager
{
    public bool IsReviewableDateReached(int requestId);
    public bool HasStatusReceived(int requestId);
    public Review CreateReview(Review review);
}