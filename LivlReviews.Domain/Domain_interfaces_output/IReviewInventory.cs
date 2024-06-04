using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IReviewInventory
{
    Review CreateReview(Review review);
}