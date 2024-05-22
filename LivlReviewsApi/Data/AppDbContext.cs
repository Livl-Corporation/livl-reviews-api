using LivlReviewsApi.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace LivlReviewsApi.Data;

public class AppDbContext : IdentityUserContext<User>
{
    
    public AppDbContext (DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public override int SaveChanges()
    {
        SetCreatedAtProperty();
        SetUpdatedAtProperty();
        
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Children)
            .WithOne(c => c.Parent)
            .HasForeignKey(c => c.ParentId);

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
                    
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
    
    // entities
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}