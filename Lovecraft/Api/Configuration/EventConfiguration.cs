using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
	public void Configure(EntityTypeBuilder<Event> builder)
	{
		builder.HasMany(ut => ut.Stories)
			.WithOne(u => u.Event)
			.HasForeignKey(ut => ut.EventId);
	}
}