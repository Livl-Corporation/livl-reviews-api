using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.RequestProduct;

public class RequestProductTests
{
    [Fact]
    public void Approve_request_reject_others_requests()
    {
        // Arrange
        Product product = new() { Id = 1, Name = "Product 1", Image = "image.jpg", URL = "http://product1.com" };
        User user = new() { Id = "1", Email = "test@test.fr", Role = Role.Admin, isConfirmed = true };
        
        UserProduct userProduct = new() { ProductId = 1, Product = product, UserId = "1", User = user };
        
        List<ProductRequest> productRequests = new();
        productRequests.Add(new ProductRequest { Id = 1, UserProductId = 1, UserProduct = userProduct, UserId = "1", State = RequestState.PENDING });
        productRequests.Add(new ProductRequest { Id = 2, UserProductId = 1, UserProduct = userProduct, UserId = "2", State = RequestState.PENDING });
        productRequests.Add(new ProductRequest { Id = 3, UserProductId = 1, UserProduct = userProduct, UserId = "3", State = RequestState.PENDING });
        productRequests.Add(new ProductRequest { Id = 4, UserProductId = 1, UserProduct = userProduct, UserId = "4", State = RequestState.PENDING });
        
        StubProductRequestInventory stubProductRequestInventory = new(productRequests);
        StockManager stockManager = new(stubProductRequestInventory);
        
        // Act
        stockManager.ApproveRequest(productRequests[0]);
        
        // Assert
        Assert.Equal(RequestState.APPROVED, productRequests[0].State);
        Assert.Equal(RequestState.REJECTED, productRequests[1].State);
        Assert.Equal(RequestState.REJECTED, productRequests[2].State);
        Assert.Equal(RequestState.REJECTED, productRequests[3].State);
    }
}