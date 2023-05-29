using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Datas.Configuration;

public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
{
	public void Configure(EntityTypeBuilder<UserTeam> builder)
	{
		builder.HasOne(ut => ut.Team)
			.WithMany(t => t.Members)
			.HasForeignKey(ut => ut.TeamId);

		builder.HasOne(ut => ut.User)
			.WithMany(u => u.UserTeams)
			.HasForeignKey(ut => ut.UserId);
	}
}