namespace Lovecraft.Api.Model;

public class Event
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Rules { get; set; }
	public string CoverUrl { get; set; }

	public DateTime DateBegin { get; set; }
	public DateTime DateEnd { get; set; }

	public virtual List<Story> stories { get; set; }

}