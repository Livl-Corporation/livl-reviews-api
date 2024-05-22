using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories;

namespace LivlReviewsApiTests.Products;

public class ProductTests : IDisposable
{
    private AppDbContext context;
    private EntityRepository<Category> categoryEntityRepository;
    private PaginatedEntityRepository<Product> productPaginatedEntityRepository;
    public ProductTests()
    {
        var factory = new LivlReviewsApiFactory();
        
        this.context = factory.CreateTestingDbContext();
        this.productPaginatedEntityRepository = new PaginatedEntityRepository<Product>(context);
        this.categoryEntityRepository = new EntityRepository<Category>(context);
        
        context.Database.EnsureCreated();

        InitializeDatabase();
    }
    public void Dispose()
    {
        // Clean up
        this.productPaginatedEntityRepository.DeleteBy(arg => true);
        this.categoryEntityRepository.DeleteBy(arg => true);
        
        context.Database.EnsureDeleted();
        context.Dispose();
    }
    
    private void InitializeDatabase()
    {
        Category cat1 = this.categoryEntityRepository.Add(new Category
        {
            Name = "Category 1"
        });
        
        Category cat2 = this.categoryEntityRepository.Add(new Category
        {
            Name = "Category 2"
        });
        
        this.productPaginatedEntityRepository.AddRange([
            new Product
            {
                Name = "Product 1",
                Image = "Image 1",
                URL = "URL 1",
                VinerURL = "Viner URL 1",
                CategoryId = cat1.Id
            },

            new Product
            {
                Name = "Product 2",
                Image = "Image 2",
                URL = "URL 2",
                VinerURL = "Viner URL 2",
                CategoryId = cat2.Id
            },

            new Product
            {
                Name = "Product 3",
                Image = "Image 3",
                URL = "URL 3",
                VinerURL = "Viner URL 3",
                CategoryId = cat1.Id
            },

            new Product
            {
                Name = "Product 4",
                Image = "Image 4",
                URL = "URL 4",
                VinerURL = "Viner URL 4",
                CategoryId = cat2.Id
            }
        ]);
    }
    
    [Fact]
    public void Get_all_products()
    {
        // Arrange
        
        // Act
        var res = productPaginatedEntityRepository.GetAll();

        // Assert
        Assert.Equal(4, res.Count);
    }

    [Fact]
    public void Get_products_with_pagination()
    {
        // Arrange
        
        // Act
        var res = productPaginatedEntityRepository.GetPaginated(new PaginationParameters
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
    }
    
    [Fact]
    public void Get_products_with_predicate()
    {
        // Arrange
        
        // Act
        var res = productPaginatedEntityRepository.GetBy(arg => arg.Name == "Product 1");
        
        // Assert
        Assert.Single(res);
        Assert.Equal("Product 1", res[0].Name);
    }

    [Fact]
    public void Get_products_by_categories()
    {
        // Arrange
        
        // Act
        var res = productPaginatedEntityRepository.GetBy(arg => arg.Category.Name == "Category 1");
        
        // Assert
        Assert.Equal(2, res.Count);
    }
    
    [Fact]
    public void Add_product()
    {
        // Arrange
        Product product = new Product
        {
            Name = "Product 5",
            Image = "Image 5",
            URL = "URL 5",
            VinerURL = "Viner URL 5"
        };
        
        // Act
        var res = productPaginatedEntityRepository.Add(product);
        
        // Assert
        Assert.Equal("Product 5", res.Name);

        // clean
        productPaginatedEntityRepository.Delete(res);
    }
}
