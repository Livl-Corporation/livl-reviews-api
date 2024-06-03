using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using Xunit;

namespace LivlReviews.Domain.Test.Entities;


public class CategoryTest
{
    [Fact]
    public void CanAdminCreateCategory()
    {
        // Arrange
        Role role = Role.Admin;
        
        // Act
        bool result = Category.Can(role, Operation.CREATE);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void CanUserReadCategory()
    {
        // Arrange
        Role role = Role.User;
        
        // Act
        bool result = Category.Can(role, Operation.READ);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void CanUserCreateCategory()
    {
        // Arrange
        Role role = Role.User;
        
        // Act
        bool result = Category.Can(role, Operation.CREATE);
        
        // Assert
        Assert.False(result);
    }
}