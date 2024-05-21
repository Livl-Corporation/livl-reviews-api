using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories;

namespace LivlReviewsApiTests.Products;

public class ProductTests
{
    // Example products
    public static IEnumerable<object[]> ExampleProducts =>
        new List<object[]>
        {
            new object[]
            {
                new List<Product>
                {
                    new Product
                    {
                        Name = "Product 1",
                        Image = "Image 1",
                        URL = "URL 1",
                        VinerURL = "Viner URL 1"
                    },
                    new Product
                    {
                        Name = "Product 2",
                        Image = "Image 2",
                        URL = "URL 2",
                        VinerURL = "Viner URL 2"
                    },
                    new Product
                    {
                        Name = "Product 3",
                        Image = "Image 3",
                        URL = "URL 3",
                        VinerURL = "Viner URL 3"
                    },
                    new Product
                    {
                        Name = "Product 4",
                        Image = "Image 4",
                        URL = "URL 4",
                        VinerURL = "Viner URL 4"
                    }
                }
            }
        };
    
    [Theory]
    [MemberData(nameof(ExampleProducts))]
    public void Get_all_products(List<Product> products)
    {
        // Arrange
        var factory = new LivlReviewsApiFactory();
        var context = factory.CreateTestingDbContext();
        var repository = new EntityRepository<Product>(context);
        
        repository.AddRange(products);        
        
        // Act
        var res = repository.GetAll();

        // Assert
        Assert.Equal(4, res.Count);
        
        // clean
        repository.DeleteBy(arg => true);
    }

    [Theory]
    [MemberData(nameof(ExampleProducts))]
    public void Get_products_with_pagination(List<Product> products)
    {
        // Arrange
        var factory = new LivlReviewsApiFactory();
        var context = factory.CreateTestingDbContext();
        var repository = new EntityRepository<Product>(context);

        repository.AddRange(products);
        
        // Act
        var res = repository.GetPaginated(new PaginationParameters
        {
            page = 1,
            pageSize = 2
        });
        
        // Assert
        Assert.Equal(1, res.Metadata.page);
        Assert.Equal(2, res.Metadata.pageSize);
        Assert.Equal(2, res.Results.Count);
        Assert.Equal(4, res.Metadata.total);
        Assert.Equal(2, res.Metadata.totalPages);
        
        // Clean
        repository.DeleteBy(arg => true);
    }
    
    [Theory]
    [MemberData(nameof(ExampleProducts))]
    public void Get_products_with_predicate(List<Product> products)
    {
        // Arrange
        var factory = new LivlReviewsApiFactory();
        var context = factory.CreateTestingDbContext();
        var repository = new EntityRepository<Product>(context);

        repository.AddRange(products);
        
        // Act
        var res = repository.GetBy(arg => arg.Name == "Product 1");
        
        // Assert
        Assert.Single(res);
        Assert.Equal("Product 1", res[0].Name);
        
        // Clean
        repository.DeleteBy(arg => true);
    }
}
