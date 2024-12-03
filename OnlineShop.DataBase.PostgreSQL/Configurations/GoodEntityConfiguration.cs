﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Models;
using OnlineShop.DataBase.PostgreSQL.Entities;

namespace OnlineShop.DataBase.PostgreSQL.Configurations
{
	public class GoodEntityConfiguration : IEntityTypeConfiguration<GoodEntity>
	{
		public void Configure(EntityTypeBuilder<GoodEntity> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.HasIndex(x => x.Name).IsUnique();
			builder.Property(x => x.Name).HasColumnType("text");

			builder.Property(x => x.Description).HasColumnType("text");
			builder.Property(x => x.Price).HasColumnType("integer");

			builder.HasOne(x => x.Category)
				.WithMany(x => x.Goods)
				.HasForeignKey(x => x.CategoryId);

			builder.HasMany(x => x.Images)
				.WithOne(x => x.Good)
				.HasForeignKey(x => x.GoodId);
		}
	}
}