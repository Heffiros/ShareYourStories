using Lovecraft.Api.Model;
using Lovecraft.Datas.Configuration;
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
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<StoryVote> StoryVotes { get; set; }
        public virtual DbSet<StoryTag> StoryTags { get; set; }
        public virtual DbSet<StoryStoryTag> StoryStoryTags { get; set; }
        public virtual DbSet<StoryComment> StoryComments { get; set; }
        public virtual DbSet<StoryComment> Badges { get; set; }

        public virtual DbSet<StoryComment> UserBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfiguration(new UserTeamConfiguration());
			modelBuilder.ApplyConfiguration(new StoryConfiguration());
			modelBuilder.ApplyConfiguration(new PageConfiguration());
			modelBuilder.ApplyConfiguration(new EventConfiguration());
			modelBuilder.ApplyConfiguration(new StoryVoteConfiguration());
			modelBuilder.ApplyConfiguration(new StoryStoryTagConfiguration());
            modelBuilder.ApplyConfiguration(new StoryCommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserBadgeConfiguration());
        }
	}
}