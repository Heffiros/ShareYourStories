using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class StoryHistoryConfiguration : IEntityTypeConfiguration<StoryHistory>
{
	public void Configure(EntityTypeBuilder<StoryHistory> builder)
	{
		builder.HasKey(sh => sh.Id);

            // Configure the unique index on the pair StoryId and UserId
            builder.HasIndex(sh => new { sh.StoryId, sh.UserId }).IsUnique();

            // Configure relationships
            builder.HasOne(sh => sh.User)
                   .WithMany(u => u.StoryHistories)
                   .HasForeignKey(sh => sh.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sh => sh.Story)
                   .WithMany(s => s.StoryHistories)
                   .HasForeignKey(sh => sh.StoryId)
                   .OnDelete(DeleteBehavior.NoAction);

	}
}

