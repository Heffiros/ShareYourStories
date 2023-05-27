using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class StoryVoteConfiguration : IEntityTypeConfiguration<StoryVote>
{
	public void Configure(EntityTypeBuilder<StoryVote> builder)
	{
		builder.HasOne(ut => ut.Story)
			.WithMany(u => u.StoryVotes)
			.HasForeignKey(ut => ut.StoryId);

		builder.HasOne(ut => ut.User)
			.WithMany(u => u.StoryVotes)
			.HasForeignKey(ut => ut.UserId);
	}
}