using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IReviewInventory
{
    bool IsReviewable(int requestId);
    Review CreateReview(Review review);
}