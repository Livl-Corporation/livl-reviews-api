using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Models;
using LivlReviews.Domain.Test.Fakes;
using LivlReviews.Domain.Test.Spies;
using Xunit;

namespace LivlReviews.Domain.Test.Entities;

public class ReviewTest
{
    [Fact]
    public void Is_Request_Reviewable_When_Reviewable_Date_Reached()
    {
        // Arrange
        var currentTime = new DateTime(2024, 5, 28);
        var fakeClock = new FakeClock(currentTime);
        var request = new Request(fakeClock)
        {
            Id = 1,
            State = RequestState.Received,
            ReviewableAt = new DateTime(2024, 5, 20) 
        };

        // Act
        var result = request.IsReviewable;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Is_Request_Not_Reviewable_When_Reviewable_Date_Not_Reached()
    {
        // Arrange
        var currentTime = new DateTime(2024, 5, 20);
        var fakeClock = new FakeClock(currentTime);
        var request = new Request(fakeClock)
        {
            Id = 1,
            State = RequestState.Received,
            ReviewableAt = new DateTime(2024, 5, 23) 
        };

        // Act
        var result = request.IsReviewable;

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Set_Request_State_To_Completed_When_Review_Created()
    {
        // Arrange
        var request = new Request(new FakeClock(new DateTime(2024, 5, 28)))
        {
            Id = 1,
            State = RequestState.Received,
            ReviewableAt = new DateTime(2024, 5, 20) 
        };
        
        var reviewInventory = new FakeReviewInventory();
        var requestInventory = new FakeRequestInventory(new List<ProductStock>(), new List<Request> { request }, new NotificationManagerSpy());
        
        NotificationManagerSpy notificationManager = new NotificationManagerSpy();
        StockManager stockManager = new StockManager(requestInventory, notificationManager);       
        
        var reviewManager = new ReviewManager(reviewInventory, stockManager, new NotificationManagerSpy());
        
        var review = new Review
        {
            RequestId = request.Id,
            Request = request,
            Id = 1,
            Rating = 5,
            Content = "Great product!",
            Title = "Great product!"
        };

        // Act
        reviewManager.CreateReview(review, request);

        // Assert
        var requestState = requestInventory.requests.Find(r => r.Id == request.Id)?.State;
        Assert.Equal(RequestState.Completed, requestState);
    }
}