using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IReviewManager
{
    public Review CreateReview(Review review, Request request);
}