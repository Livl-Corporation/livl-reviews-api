using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class ReviewManager(IReviewInventory reviewInventory, IStockManager stockManager, INotificationManager notificationManager) : IReviewManager
{
    public Review CreateReview(Review review, Request request)
    {
        if(!request.IsReviewable)
        {
            throw new Exception("The request is not reviewable.");
        }
        
        var createdReview = reviewInventory.CreateReview(review);
        
        stockManager.UpdateRequestState(request, RequestState.Completed);
        
        createdReview.Request = request;
        notificationManager.SendNotificationToAdminWhenReviewSubmitted(createdReview);
        
        return createdReview;
    }
}