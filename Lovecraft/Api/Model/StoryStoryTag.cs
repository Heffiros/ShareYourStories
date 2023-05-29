namespace Lovecraft.Api.Model;

public class StoryStoryTag
{
	public int Id { get; set; }
	public int StoryId { get; set; }
	public int StoryTagId { get; set; }

	public virtual Story Story { get; set; }
	public virtual StoryTag StoryTag { get; set; }
}