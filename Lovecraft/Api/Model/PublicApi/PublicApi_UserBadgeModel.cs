using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
    public class PublicApi_UserBadgeModel
    {
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "badgeId")]
        public int BadgeId { get; set; }

    }
}
