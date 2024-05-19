using LivlReviewsApi;
using LivlReviewsApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LivlReviewsApiTests;

public class LivlReviewsApiFactory : WebApplicationFactory<IApiAssemblyMarker>
{

    public LivlReviewsApiFactory()
    {
        
    }
    public AppDbContext CreateTestingDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "livlTestDatabase")
            .Options;
        var dbContext = new AppDbContext(options);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var dbContext = CreateTestingDbContext();
        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<AppDbContext>(_ => dbContext);
        });
    }
}