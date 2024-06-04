using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Fakes;

public class FakeReviewInventory() : IReviewInventory
{
    public Review CreateReview(Review review)
    {
        return review;
    }
}