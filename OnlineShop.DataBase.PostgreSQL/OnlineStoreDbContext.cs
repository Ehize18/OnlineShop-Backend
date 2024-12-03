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
		public DbSet<GoodEntity> GoodEntity { get; set; }
		public DbSet<ImageEntity> Images { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Basket> Baskets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Good>(b =>
			{
				b.HasKey(x => x.Id);
				b.ToTable(nameof(GoodEntity));
			});

			modelBuilder.Entity<Image>(b =>
			{
				b.HasKey(x => x.Id);
				b.ToTable(nameof(Images));;
			});

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
