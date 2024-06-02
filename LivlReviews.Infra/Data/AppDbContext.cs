using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LivlReviews.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<User>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }
    public DbSet<ProductRequest> ProductRequests { get; set; }
    public DbSet<InvitationToken> InvitationTokens { get; set; }
    
    public override int SaveChanges()
    {
        SetCreatedAtProperty();
        SetUpdatedAtProperty();
        
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // UserProduct
        modelBuilder.Entity<UserProduct>()
            .HasKey(up => new { up.ProductId, up.UserId });

        modelBuilder.Entity<UserProduct>()
            .HasOne(up => up.Product)
            .WithMany(p => p.UserProducts)
            .HasForeignKey(up => up.ProductId);

        modelBuilder.Entity<UserProduct>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserProducts)
            .HasForeignKey(up => up.UserId);
        
        // ProductRequest
        modelBuilder.Entity<ProductRequest>()
            .HasKey(pr => pr.Id);

        modelBuilder.Entity<ProductRequest>()
            .HasOne(pr => pr.User)
            .WithMany(u => u.ProductRequests)
            .HasForeignKey(pr => pr.UserId);

        modelBuilder.Entity<ProductRequest>()
            .HasOne(pr => pr.Product)
            .WithMany(p => p.ProductRequests)
            .HasForeignKey(pr => pr.ProductId);
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
}