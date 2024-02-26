using Lovecraft.Model;

namespace Lovecraft.Api.Model;

public class StoryComment
{
	public int Id { get; set; }
	public string Text { get; set; }

	public DateTime DateCreated { get; set; }

	public int UserId { get; set; }
	public int StoryId { get; set; }
	public Status Status { get; set; }
	public virtual User User { get; set; }
	public virtual Story Story { get; set; }
}