using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class StoryStoryTagConfiguration : IEntityTypeConfiguration<StoryStoryTag>
{
	public void Configure(EntityTypeBuilder<StoryStoryTag> builder)
	{
		builder.HasOne(sst => sst.Story )
			.WithMany(u => u.StoryStoryTags)
			.HasForeignKey(ut => ut.StoryId);

		builder.HasOne(sst => sst.StoryTag)
			.WithMany(u => u.StoryStoryTags)
			.HasForeignKey(ut => ut.StoryTagId);
	}
}

