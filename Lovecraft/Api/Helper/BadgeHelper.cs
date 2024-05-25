using Lovecraft.Api.Model;

namespace Lovecraft.Api.Helper;
public class BadgeHelper
{
    private readonly ILovecraftUnitOfWork _luow;
    public BadgeHelper(ILovecraftUnitOfWork luow)
    {
        _luow = luow;
    }

    public async Task GiveUserBadge(int userId, int badgeId)
    {
        var userBadge = new UserBadge
        {
            UserId = userId,
            BadgeId = badgeId
        };
        
        _luow.UserBadges.Add(userBadge);
        await _luow.SaveChangesAsync();
    }
}