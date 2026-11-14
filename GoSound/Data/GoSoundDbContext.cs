using GoSound.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace GoSound.Data;
public class GoSoundDbContext(DbContextOptions<GoSoundDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
 public DbSet<Category> Categories => Set<Category>();
 public DbSet<Podcast> Podcasts => Set<Podcast>();
 public DbSet<Episode> Episodes => Set<Episode>();
 public DbSet<Follow> Follows => Set<Follow>();
 public DbSet<ListeningProgress> ListeningProgresses => Set<ListeningProgress>();
 protected override void OnModelCreating(ModelBuilder b)
 {
  base.OnModelCreating(b);
  b.Entity<Category>().HasIndex(c => c.Name).IsUnique();
  b.Entity<Podcast>().HasOne(p => p.Owner).WithMany().HasForeignKey(p => p.OwnerId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Podcast>().HasOne(p => p.Category).WithMany(c => c.Podcasts).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Episode>().HasOne(e => e.Podcast).WithMany(p => p.Episodes).HasForeignKey(e => e.PodcastId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Follow>().HasKey(f => new { f.UserId, f.PodcastId });
  b.Entity<Follow>().HasOne(f => f.User).WithMany().HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Follow>().HasOne(f => f.Podcast).WithMany().HasForeignKey(f => f.PodcastId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<ListeningProgress>().HasKey(p => new { p.UserId, p.EpisodeId });
  b.Entity<ListeningProgress>().HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<ListeningProgress>().HasOne(p => p.Episode).WithMany().HasForeignKey(p => p.EpisodeId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<ListeningProgress>().ToTable(t => t.HasCheckConstraint("CK_Progress_Position", "[PositionSeconds] >= 0"));
  b.Entity<Category>().HasData(new Category {Id=1,Name="Технологии"},new Category {Id=2,Name="Бизнес"},new Category {Id=3,Name="Образование"},new Category {Id=4,Name="Култура"},new Category {Id=5,Name="Спорт"});
 }
}
