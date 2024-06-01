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
        this.DefineRequiredProperties(modelBuilder);
        this.CreateRelationships(modelBuilder);
        
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
    
    private void CreateRelationships(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Children)
            .WithOne(c => c.Parent)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
    
    private void DefineRequiredProperties(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).IsRequired(false);
            entity.Property(e => e.Name).IsRequired(true);
            entity.Property(e => e.Image).IsRequired(true);
            entity.Property(e => e.URL).IsRequired(true);
            entity.Property(e => e.VinerURL).IsRequired(true);
            entity.Property(e => e.CategoryId).IsRequired(true);
            entity.Property(e => e.CreatedAt).IsRequired(false);
            entity.Property(e => e.UpdatedAt).IsRequired(false);
            entity.Property(e => e.DeletedAt).IsRequired(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).IsRequired(false);
            entity.Property(e => e.Name).IsRequired(true);
            entity.Property(e => e.ParentId).IsRequired(false);
            entity.Property(e => e.Products).IsRequired(false);
            entity.Property(e => e.Children).IsRequired(false);
        });
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}