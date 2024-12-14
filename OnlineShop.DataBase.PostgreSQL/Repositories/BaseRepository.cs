namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class BaseRepository
	{
		private readonly OnlineStoreDbContext _dbContext;
		public BaseRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
	}
}
