using DataContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementCategory> AnnouncementCategories { get; set; }
        
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Announcement>()
                .HasKey(a => new {a.Id});
            
            modelBuilder.Entity<Announcement>()
                .Property(b => b.PublishedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<AnnouncementCategory>()
                .HasKey(a => new {a.Id});
            
            
        }
    }
}