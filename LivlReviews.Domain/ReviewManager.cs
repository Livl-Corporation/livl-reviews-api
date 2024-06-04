using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class ReviewManager(IReviewInventory reviewInventory, IStockManager stockManager) : IReviewManager
{
    public bool IsReviewableDateReached(int requestId)
    {
        return reviewInventory.IsReviewableDateReached(requestId);
    }
    
    public bool HasStatusReceived(int requestId)
    {
        return reviewInventory.HasStatusReceived(requestId);
    }
    
    public Review CreateReview(Review review)
    {
        if(!IsReviewableDateReached(review.RequestId))
        {
            throw new Exception("The request is not reviewable yet, please wait until the reviewable date.");
        }
        
        if(!HasStatusReceived(review.RequestId))
        {
            throw new Exception("The request should be in the received state to create a review.");
        }
        
        var createdReview = reviewInventory.CreateReview(review);
        stockManager.UpdateRequestState(review.Request, RequestState.Completed);

        return createdReview;
    }
}