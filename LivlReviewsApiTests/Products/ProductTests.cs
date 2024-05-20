using System.Net;
using System.Net.Http.Json;

namespace LivlReviewsApiTests.Products;

public class ProductTests
{
    [Fact]
    public async Task Should_return_200_when_getting_products()
    {
        var api = new LivlReviewsApiFactory();
        var httpClient = api.CreateClient();
        
        var response = await httpClient.GetAsync("/api/products");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Pagination_should_work()
    {
        var api = new LivlReviewsApiFactory();
        var httpClient = api.CreateClient();
        
        var response = await httpClient.GetAsync("/api/products?page=2&pageSize=2");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Should_return_200_when_create_product()
    {
        var api = new LivlReviewsApiFactory();
        var httpClient = api.CreateClient();
        
        var response = await httpClient.PostAsJsonAsync("/api/products", new
        {
            Name = "Product 1",
            Image = "https://via.placeholder.com/150",
            URL = "https://www.google.com",
            Categories = new List<string> { "Category 1", "Category 2" }
        });
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}