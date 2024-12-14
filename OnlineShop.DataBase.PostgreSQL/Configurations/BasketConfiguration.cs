using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DataBase.PostgreSQL.Configurations
{

	public class BasketConfiguration : IEntityTypeConfiguration<Basket>
	{
		public void Configure(EntityTypeBuilder<Basket> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.isCompleted).HasColumnType("boolean");

			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");

			builder.HasOne(x => x.User)
				.WithMany(x => x.Baskets)
				.HasForeignKey(x => x.UsertId);

			builder.HasMany(x => x.Items)
				.WithOne(x => x.Basket)
				.HasForeignKey(x => x.BasketId);
		}
	}
}
