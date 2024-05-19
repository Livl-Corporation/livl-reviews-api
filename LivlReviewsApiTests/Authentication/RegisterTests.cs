using System.Net;
using System.Net.Http.Json;
using LivlReviewsApi.Controllers;
using LivlReviewsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LivlReviewsApiTests;

public class RegisterTests
{
    private ServiceProvider _serviceProvider;

    [Fact]
    public async Task Should_return_200_when_registering_user()
    {
        var api = new LivlReviewsApiFactory();
        var httpClient = api.CreateClient();
        
        var response = await httpClient.PostAsJsonAsync("/api/users/register", new
        {
            Email = "lsqdmkjflmkdsqjfmlkjsqaplqsdfqdsfqsdfqdslkf@email.com",
            Password = "12345azertydsqfmlkj"
        });
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}