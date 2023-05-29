using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
	public void Configure(EntityTypeBuilder<Page> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Content).IsRequired();
		builder.Property(p => p.LastUpdatedDateTime).IsRequired();

		builder.HasOne(p => p.Story)
			.WithMany(s => s.Pages)
			.HasForeignKey(ut => ut.StoryId);
	}
}