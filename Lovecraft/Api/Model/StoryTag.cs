namespace Lovecraft.Api.Model
{
	public class StoryTag
	{
		public int Id { get; set; }
		public string Label { get; set; }

		public virtual List<StoryStoryTag> StoryStoryTags { get; set; }
	}
}
