using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Entities;

public class RequestTest
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
        
        FakeUser admin = new FakeUser()
        {
            Id = "1",
            Email = "User 1",
            InvitedById = null
        };

        FakeUser requester = new FakeUser()
        {
            Id = "2",
            Email = "User 2",
            InvitedById = "1"
        };

        ProductStock stock = new ProductStock
        {
            ProductId = 1,
            AdminId = "1"
        };
    
        StubRequestInventory requestInventory = new StubRequestInventory(stock);
        
        StockManager stockManager = new StockManager(requestInventory);
        
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
            InvitedById = null
        };

        ProductStock stock = new ProductStock
        {
            ProductId = 1,
            AdminId = "1"
        };
    
        StubRequestInventory requestInventory = new StubRequestInventory(stock);
        
        StockManager stockManager = new StockManager(requestInventory);
        
        // Act
        bool isRequestable = stockManager.IsRequestable(product, admin);

        // Assert
        Assert.False(isRequestable);
    }
}