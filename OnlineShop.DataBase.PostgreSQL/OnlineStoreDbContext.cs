using Microsoft.EntityFrameworkCore;
using OnlineShop.DataBase.PostgreSQL.Configurations;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL
{
	public class OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options)
		: DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<GoodCategory> GoodCategories { get; set; }
		public DbSet<Good> GoodEntity { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Basket> Baskets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GoodCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new GoodEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ImageConfiguration());
			modelBuilder.ApplyConfiguration(new BasketConfiguration());
			modelBuilder.ApplyConfiguration(new BasketItemConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
