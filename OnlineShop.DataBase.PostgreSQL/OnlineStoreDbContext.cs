using Microsoft.EntityFrameworkCore;
using OnlineShop.DataBase.PostgreSQL.Configurations;
using OnlineShop.Core.Models;
using OnlineShop.DataBase.PostgreSQL.Entities;

namespace OnlineShop.DataBase.PostgreSQL
{
	public class OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options)
		: DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<GoodCategoryEntity> GoodCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GoodCategoryConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
