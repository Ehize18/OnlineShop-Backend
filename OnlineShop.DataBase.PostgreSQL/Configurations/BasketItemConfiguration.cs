﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Configurations
{
	public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
	{
		public void Configure(EntityTypeBuilder<BasketItem> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");

			builder.HasOne(x => x.Basket)
				.WithMany(x => x.Items)
				.HasForeignKey(x => x.BasketId);

			builder.HasOne(x => x.Good)
				.WithMany()
				.HasForeignKey(x => x.GoodId);
		}
	}
}
