using DatingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DatingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DatingApp;
public class AppDbContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    public DbSet<Photo> Photos { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Message { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<IdentityUserLogin<int>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().HasNoKey();

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        // Configure Like entity
        modelBuilder.Entity<Like>()
            .HasKey(k => new { k.LikerId, k.LikeeId });

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Likee)
            .WithMany(u => u.Likers)
            .HasForeignKey(l => l.LikeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Liker)
            .WithMany(u => u.Likees)
            .HasForeignKey(l => l.LikerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Message entity
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.MessageSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Recipient)
            .WithMany(u => u.MessagesRecieved)
            .HasForeignKey(m => m.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
    }
}