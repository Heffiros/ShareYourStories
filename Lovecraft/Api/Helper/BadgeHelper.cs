using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Lovecraft.Api.Model;
using Lovecraft.Api.Repository;

namespace Lovecraft.Api.Helper;
public class BadgeHelper
{
    private readonly ICommonRepository<UserBadge> _userBadgeRepository;
    public BadgeHelper(ICommonRepository<UserBadge> userBadgeRepository)
    {
        _userBadgeRepository = userBadgeRepository;
    }

    public async Task GiveUserBadge(int userId, int badgeId)
    {
        var userBadge = new UserBadge
        {
            UserId = userId,
            BadgeId = badgeId
        };
        
        _userBadgeRepository.Add(userBadge);
        await _userBadgeRepository.SaveAsync();
    }
}