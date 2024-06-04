﻿using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Clock;
using LivlReviews.Domain.Test.Fakes;
using LivlReviews.Domain.Test.Spies;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Entities;

public class ReviewTest
{
    [Fact]
    public void Is_Request_Reviewable_When_Reviewable_Date_Reached()
    {
        // Arrange
        var request = new Request
        {
            Id = 1,
            State = RequestState.Received,
            ReviewableAt = new DateTime(2024, 5, 20) 
        };

        var fakeClock = new FakeClock(new DateTime(2024, 5, 28)); 

        var reviewInventory = new FakeReviewInventory(request, fakeClock);

        // Act
        var result = reviewInventory.IsReviewableDateReached(request.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Is_Request_Not_Reviewable_When_Reviewable_Date_Not_Reached()
    {
        // Arrange
        var request = new Request
        {
            Id = 1,
            State = RequestState.Received,
            ReviewableAt = new DateTime(2024, 5, 23) 
        };

        var fakeClock = new FakeClock(new DateTime(2024, 5, 20)); // 3 days before the reviewable date

        var reviewInventory = new FakeReviewInventory(request, fakeClock);

        // Act
        var result = reviewInventory.IsReviewableDateReached(request.Id);

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
            ReviewableAt = new DateTime(2024, 5, 20) 
        };

        var fakeClock = new FakeClock(new DateTime(2024, 5, 28));

        var reviewInventory = new FakeReviewInventory(request, fakeClock);
        var requestInventory = new FakeRequestInventory(new List<ProductStock>(), new List<Request> { request }, new NotificationManagerSpy());
        
        NotificationManagerSpy notificationManager = new NotificationManagerSpy();
        StockManager stockManager = new StockManager(requestInventory, notificationManager);       
        
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