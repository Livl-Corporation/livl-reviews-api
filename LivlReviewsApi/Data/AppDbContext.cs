using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LivlReviewsApi.Data;

public class AppDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public AppDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connexion string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection"));
    }
    
    public DbSet<User> Users { get; set; }
}