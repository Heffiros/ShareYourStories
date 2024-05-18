using Lovecraft.Model;

namespace Lovecraft.Api.Model
{
    public class UserBadge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BadgeId { get; set; }

        public virtual Badge Badge { get; set; }

        public virtual User User { get; set; }
    }
}
