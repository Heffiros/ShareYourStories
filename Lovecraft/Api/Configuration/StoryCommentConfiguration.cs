using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lovecraft.Datas.Configuration;

public class StoryCommentConfiguration : IEntityTypeConfiguration<StoryComment>
{
	public void Configure(EntityTypeBuilder<StoryComment> builder)
	{
		builder.HasOne(sc => sc.User)
			.WithMany(u => u.StoryComments)
			.HasForeignKey(sc => sc.UserId);

		builder.HasOne(sc => sc.Story)
			.WithMany(u => u.StoryComments)
			.HasForeignKey(sc => sc.StoryId);
	}
}