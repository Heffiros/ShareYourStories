using Lovecraft.Model;

namespace Lovecraft.Api.Model
{
	public class Page
	{
		public int Id { get; set; }
		public int Order { get; set; }
		public string Content { get; set; }
		public DateTime LastUpdatedDateTime { get; set; }
		public int StoryId { get; set; }

		public virtual Story Story { get; set; }
	}
}
