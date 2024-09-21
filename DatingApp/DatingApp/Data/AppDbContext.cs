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
        modelBuilder.Entity<UserRole> ()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
        
        modelBuilder.Entity<Like>()
            .HasKey(k => new { k.LikerId, k.LikeeId });

        modelBuilder.Entity<Like>()
            .HasOne(u => u.Likee)
            .WithMany(u => u.Likers)
            .HasForeignKey(u => u.LikeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Like>()
            .HasOne(u => u.Liker)
            .WithMany(u => u.Likees)
            .HasForeignKey(u => u.LikeeId)
            .OnDelete(DeleteBehavior.Restrict);
        

        modelBuilder.Entity<Message>()
            .HasOne(u => u.Sender)
            .WithMany(m => m.MessageSent)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(u => u.Recipient)
            .WithMany(m => m.MessagesRecieved)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
    }
}
