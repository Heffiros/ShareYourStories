namespace Lovecraft.Api.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public string ? TeamLogoUrl { get; set; }

        public virtual List<UserTeam> Members { get; set; }

        public virtual List<Story> Stories { get; set; }
    }
}
