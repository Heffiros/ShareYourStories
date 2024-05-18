using Lovecraft.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lovecraft.Datas.Configuration;

public class UserBadgeConfiguration : IEntityTypeConfiguration<UserBadge>
{
    public void Configure(EntityTypeBuilder<UserBadge> builder)
    {
        builder.HasOne(ub => ub.User)
            .WithMany(u => u.UserBadges)
            .HasForeignKey(ub => ub.UserId);

        builder.HasOne(ub => ub.Badge)
            .WithMany(u => u.UserBadges)
            .HasForeignKey(ub => ub.BadgeId);
    }
}

