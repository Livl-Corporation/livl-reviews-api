using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Fakes;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Entities;

public class RequestTests
{
    [Fact]
    public void Is_Product_Requestable()
    {
        // Arrange
        Product product = new Product
        {
            Id = 1,
            Name = "Product 1",
            URL = "http://product1.com",
        };

        FakeUser admin = UsersStub.Admin;

        FakeUser requester = new FakeUser()
        {
            Id = "2",
            Email = "User 2",
            InvitedByTokenId = 1,
            InvitedByToken = InvitationTokensStub.InvitationToken,
        };

        ProductStock stock = new ProductStock
        {
            ProductId = 1,
            AdminId = UsersStub.Admin.Id
        };
    
        FakeRequestInventory requestInventory = new FakeRequestInventory([stock], []);
        FakeStockInventory stockInventory = new FakeStockInventory([stock]);
        
        StockManager stockManager = new StockManager(requestInventory, stockInventory);
        
        // Act
        bool isRequestable = stockManager.IsRequestable(product, requester);

        // Assert
        Assert.True(isRequestable);
    }
    
    [Fact]
    public void Is_Product_Not_Requestable_for_admin()
    {
        // Arrange
        Product product = new Product
        {
            Id = 1,
            Name = "Product 1",
            URL = "http://product1.com",
        };
        
        IUser admin = new FakeUser
        {
            Id = "1",
            Email = "User 1",
            InvitedByTokenId = null
        };

        ProductStock stock = new ProductStock
        {
            ProductId = 1,
            AdminId = "1"
        };
    
        FakeRequestInventory requestInventory = new FakeRequestInventory([stock], []);
        FakeStockInventory stockInventory = new FakeStockInventory([stock]);
        
        StockManager stockManager = new StockManager(requestInventory, stockInventory);
        
        // Act
        bool isRequestable = stockManager.IsRequestable(product, admin);

        // Assert
        Assert.False(isRequestable);
    }

    [Fact]
    public void Approve_a_request_reject_other_request_on_same_product()
    {
        // Arrange
        FakeUser requester = new FakeUser
        {
            Id = "2",
            Email = "User 2",
            InvitedByTokenId = 1,
            InvitedByToken = InvitationTokensStub.InvitationToken,
        };
        
        ProductStock stock = new ProductStock
        {
            AdminId = "1",
            ProductId = 1
        };
        
        Request firstRequest = new Request
        {
            Id = 1,
            ProductId = 1,
            AdminId = "1",
            UserId = "2",
            State = RequestState.Pending
        };

        Request secondRequest = new Request
        {
            Id = 2,
            ProductId = 1,
            AdminId = "1",
            UserId = "3",
            State = RequestState.Pending
        };

        FakeRequestInventory requestInventory = new FakeRequestInventory([stock], [firstRequest, secondRequest]);
        FakeStockInventory stockInventory = new FakeStockInventory([stock]);
        
        StockManager stockManager = new StockManager(requestInventory, stockInventory);
        
        // Act
        Request approvedRequest = stockManager.ApproveRequest(firstRequest, requester);
        
        // Assert
        Assert.Equal(RequestState.Approved, requestInventory.requests[0].State);
        Assert.Equal(RequestState.Rejected, requestInventory.requests[1].State);
    }
    
    [Fact]
    public void Approve_a_request_remove_stock()
    {
        // Arrange
        FakeUser requester = new FakeUser
        {
            Id = "2",
            Email = "User 2",
            InvitedByTokenId = 1,
            InvitedByToken = InvitationTokensStub.InvitationToken,
        };
        
        ProductStock stock = new ProductStock
        {
            AdminId = "1",
            ProductId = 1
        };
        
        Request firstRequest = new Request
        {
            Id = 1,
            ProductId = 1,
            AdminId = "1",
            UserId = "2",
            State = RequestState.Pending
        };

        FakeRequestInventory requestInventory = new FakeRequestInventory([stock], [firstRequest]);
        FakeStockInventory stockInventory = new FakeStockInventory([stock]);
        
        StockManager stockManager = new StockManager(requestInventory, stockInventory);
        
        // Act
        Request approvedRequest = stockManager.ApproveRequest(firstRequest, requester);
        
        // Assert
        Assert.Empty(requestInventory.stocks);
    }
}