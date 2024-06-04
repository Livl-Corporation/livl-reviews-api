using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IReviewInventory
{
    bool IsReviewableDateReached(int requestId);
    Review CreateReview(Review review);
    bool HasStatusReceived(int requestId);
}