namespace Lovecraft.Api.Model
{
    public class Badge
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string EmptyBadgeUrl { get; set; }
        public string BadgeUrl { get; set; }

        public virtual List<UserBadge> UserBadges { get; set; }
    }
}
