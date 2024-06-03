using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IReviewManager
{
    public bool IsReviewable(int requestId);

    public Review CreateReview(Review review);
}