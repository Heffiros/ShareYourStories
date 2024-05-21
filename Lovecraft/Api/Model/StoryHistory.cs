using Lovecraft.Model;

namespace Lovecraft.Api.Model;

public class StoryHistory
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public int StoryId { get; set; }
    public int LastPageReadId { get; set; }
    public int Reread { get; set; }
	public HistoryState State { get; set; }

    public DateTime Date { get; set; }
	public virtual User User { get; set; }
	public virtual Story Story { get; set; }
	public virtual Page LastPageRead { get; set; }

    public enum HistoryState
    {
        Reading,
        Endend,
        Abandonned
    }
}