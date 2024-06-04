using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class ReviewManager(IReviewInventory reviewInventory, IStockManager stockManager) : IReviewManager
{
    public bool IsReviewable(int requestId)
    {
        return reviewInventory.IsReviewable(requestId);
    }
    
    public Review CreateReview(Review review)
    {
        // if(!IsReviewable(review.RequestId))
        // {
        //     throw new Exception("Request is not in a reviewable state. Cannot create review.");
        // }
        
        var createdReview = reviewInventory.CreateReview(review);
        stockManager.UpdateRequestState(review.Request, RequestState.Completed);

        return createdReview;
    }
}