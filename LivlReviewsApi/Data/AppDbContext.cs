using System.Net.Mail;
using LivlReviewsApi.Data.Interfaces;
using LivlReviewsApi.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace LivlReviewsApi.Data;

public class AppDbContext : IdentityUserContext<User>
{
    protected readonly IConfiguration Configuration;
    
    public AppDbContext (DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public override int SaveChanges()
    {
        SetCreatedAtProperty();
        SetUpdatedAtProperty();
        
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // SHould work but it's not
        var defaultAdminEmail = Configuration.GetSection("SiteSettings:AdminEmail").Value;
        var userName = new MailAddress(defaultAdminEmail!).User;
        var defaultAdminPassword = Configuration.GetSection("SiteSettings:AdminPassword").Value;
        var defaultUser = new User { Email = defaultAdminEmail, UserName = userName, Role = Role.Admin };

        PasswordHasher<User> ph = new PasswordHasher<User>();
        defaultUser.PasswordHash = ph.HashPassword(defaultUser, defaultAdminPassword!);
        
        modelBuilder.Entity<User>().HasData(defaultUser);
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
    public DbSet<InvitationToken> InvitationTokens { get; set; }
}