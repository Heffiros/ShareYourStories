using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
	public void Configure(EntityTypeBuilder<Story> builder)
	{
		builder.HasOne(ut => ut.User)
			.WithMany(u => u.Stories)
			.HasForeignKey(ut => ut.UserId);

		builder.HasOne(ut => ut.Team)
			.WithMany(u => u.Stories)
			.HasForeignKey(ut => ut.TeamId);
	}
}