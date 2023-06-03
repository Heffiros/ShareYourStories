using Lovecraft.Datas;

namespace Lovecraft.Api.Repository;

public class StoryCommentRepository
{
	readonly LovecraftDbContext _dbContext = new();

	public StoryCommentRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}
}