using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LivlReviewsApi.Data;

public class AppDbContext : IdentityUserContext<User>
{
    // public DbSet<Page> Pages => Set<Page>();
    
    public AppDbContext (DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}