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
        CreateRelationships(modelBuilder);
        ConfigureEntityConstraints(modelBuilder);
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

        modelBuilder.Entity<Request>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Requests)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Request>()
            .HasOne<User>(r => r.User as User)
            .WithMany(u => u.SubmittedRequests)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);
            
        modelBuilder.Entity<Request>()
            .HasOne<User>(r => r.Admin as User)
            .WithMany(u => u.ReceivedRequests)
            .HasForeignKey(r => r.AdminId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<ProductStock>()
            .HasKey(ps => new { ps.ProductId, ps.AdminId });

        modelBuilder.Entity<ProductStock>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.Stocks)
            .HasForeignKey(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<ProductStock>()
            .HasOne<User>(ps => ps.Admin as User)
            .WithMany(u => u.Stocks)
            .HasForeignKey(ps => ps.AdminId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ProductStock>()
            .HasOne<Import>(p => p.Import);
        
        modelBuilder.Entity<InvitationToken>()
            .HasOne(i => i.InvitedUser as User)
            .WithOne(u => u.InvitedByToken)
            .HasForeignKey<InvitationToken>(i => i.InvitedUserId);

        modelBuilder.Entity<InvitationToken>()
            .HasOne(i => i.InvitedByUser as User)
            .WithMany(u => u.CreatedInvitationTokens)
            .HasForeignKey(i => i.InvitedByUserId);
        
        modelBuilder.Entity<Review>()
            .HasOne<Request>(r => r.Request);

        modelBuilder.Entity<Import>()
            .HasOne<User>(i => i.Admin as User)
            .WithMany(u => u.Imports)
            .HasForeignKey(i => i.AdminId);
        
        
    }
    
    private void ConfigureEntityConstraints(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(r => r.Title)
                .HasMaxLength(255);

            entity.Property(r => r.Content)
                .HasMaxLength(10000);
        });
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<InvitationToken> InvitationTokens { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ProductStock> Stocks { get; set; }
    public DbSet<Import> Imports { get; set; }
}