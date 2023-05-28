using Lovecraft.Model;

namespace Lovecraft.Api.Model
{
	public class StoryVote
	{
		public int Id { get; set; }
		public int StoryId { get; set; }
		public DateTime DateVoted { get; set; }
		public int UserId { get; set; }

		public virtual Story Story { get; set; }
		public virtual User User { get; set; }
	}
}
