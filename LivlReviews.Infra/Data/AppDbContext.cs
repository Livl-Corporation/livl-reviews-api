using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LivlReviews.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<User>(options)
{
    public override int SaveChanges()
    {
        SetCreatedAtProperty();
        SetUpdatedAtProperty();
        
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    private void SetCreatedAtProperty()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Metadata.GetProperties().Any(p => p.Name == "CreatedAt"));

        foreach (var entry in entries)
        {
            ((ICreatedDate) entry.Entity).CreatedAt = DateTime.Now;
        }
    }

    private void SetUpdatedAtProperty()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified  && e.Metadata.GetProperties().Any(p => p.Name == "UpdatedAt"));

        foreach (var entry in entries)
        {
            ((IUpdatedDate) entry.Entity).UpdatedAt = DateTime.Now;
        }
    }
    
    public DbSet<Product> Products { get; set; }
}