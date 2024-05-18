using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
    public class PublicApi_BadgeModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "emptyBadgeUrl")]
        public string EmptyBadgeUrl { get; set; }

        [DataMember(Name = "badgeUrl")]
        public string BadgeUrl { get; set; }

        [DataMember(Name = "userBadges")]
        public List<PublicApi_UserBadgeModel> ?UserBadges { get; set; }
    }
}
