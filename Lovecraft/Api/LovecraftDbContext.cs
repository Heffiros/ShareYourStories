using Lovecraft.Api.Model;
using Lovecraft.Model;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas
{
    public class LovecraftDbContext : DbContext
    {
        public LovecraftDbContext()
        {

        }
        public LovecraftDbContext(DbContextOptions<LovecraftDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<UserTeam> UserTeams { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(ut => ut.TeamId);

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.UserId);
            modelBuilder.Entity<Story>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.Stories)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<Story>()
                .HasOne(ut => ut.Team)
                .WithMany(u => u.Stories)
                .HasForeignKey(ut => ut.TeamId);

            modelBuilder.Entity<Page>()
	            .HasKey(p => p.Id);
            modelBuilder.Entity<Page>().Property(p => p.Content).IsRequired();
            modelBuilder.Entity<Page>().Property(p => p.LastUpdatedDateTime).IsRequired();
            modelBuilder.Entity<Page>()
	            .HasOne(p => p.Story)
	            .WithMany(s => s.Pages)
	            .HasForeignKey(ut => ut.StoryId);

		}
	}
}