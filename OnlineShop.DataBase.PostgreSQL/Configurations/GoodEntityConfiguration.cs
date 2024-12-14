using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Configurations
{
	public class GoodEntityConfiguration : IEntityTypeConfiguration<Good>
	{
		public void Configure(EntityTypeBuilder<Good> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.HasIndex(x => x.Name).IsUnique();
			builder.Property(x => x.Name).HasColumnType("text");

			builder.Property(x => x.Description).HasColumnType("text");
			builder.Property(x => x.Price).HasColumnType("integer");

			builder.Property(x => x.Count).HasDefaultValue(0);

			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");

			builder.HasOne(x => x.Category)
				.WithMany(x => x.Goods)
				.HasForeignKey(x => x.CategoryId);

			builder.HasMany(x => x.Images)
				.WithOne(x => x.Good)
				.HasForeignKey(x => x.GoodId);
			builder.HasMany<BasketItem>()
				.WithOne(x => x.Good)
				.HasForeignKey(x => x.GoodId);
		}
	}
}
