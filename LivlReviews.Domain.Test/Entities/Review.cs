using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Clock;
using LivlReviews.Domain.Test.Fakes;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Entities;

public class ReviewTest
{
    [Fact]
    public void Is_Request_Reviewable_When_Received_Over_Seven_Days_Ago()
    {
        // Arrange
        var request = new Request
        {
            Id = 1,
            State = RequestState.Received,
            ReceivedAt = new DateTime(2024, 5, 20) 
        };

        var fakeClock = new FakeClock(new DateTime(2024, 5, 28)); // 8 days after 

        var reviewInventory = new FakeReviewInventory(request, fakeClock);

        // Act
        var result = reviewInventory.IsReviewable(request.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Is_Request_Not_Reviewable_When_Received_Less_Than_Seven_Days_Ago()
    {
        // Arrange
        var request = new Request
        {
            Id = 1,
            State = RequestState.Received,
            ReceivedAt = new DateTime(2024, 5, 23) // May 23, 2024
        };

        var fakeClock = new FakeClock(new DateTime(2024, 5, 28)); // 5 days after

        var reviewInventory = new FakeReviewInventory(request, fakeClock);

        // Act
        var result = reviewInventory.IsReviewable(request.Id);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Set_Request_State_To_Completed_When_Review_Created()
    {
        // Arrange
        var request = new Request
        {
            Id = 1,
            State = RequestState.Received,
            ReceivedAt = new DateTime(2024, 5, 20) 
        };

        var fakeClock = new FakeClock(new DateTime(2024, 5, 28)); // 8 days after 

        var reviewInventory = new FakeReviewInventory(request, fakeClock);
        var requestInventory = new FakeRequestInventory(new List<ProductStock>(), new List<Request> { request });
        var stockManager = new StockManager(requestInventory);
        
        var reviewManager = new ReviewManager(reviewInventory, stockManager);
        
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
        reviewManager.CreateReview(review);

        // Assert
        var requestState = requestInventory.requests.Find(r => r.Id == request.Id)?.State;
        Assert.Equal(RequestState.Completed, requestState);
    }
}