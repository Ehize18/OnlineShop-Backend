﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Models;


namespace OnlineShop.DataBase.PostgreSQL.Configurations
{
	public class GoodCategoryConfiguration : IEntityTypeConfiguration<GoodCategory>
	{
		public void Configure(EntityTypeBuilder<GoodCategory> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.HasIndex(x => x.Title).IsUnique();
			builder.Property(x => x.Title).HasColumnType("varchar(30)");
			builder.Property(x => x.Description).HasColumnType("text");
			builder.Property(x => x.ParentId).HasColumnType("integer");

			builder.Ignore(x => x.Childs);

			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");

			builder.HasMany(x => x.Goods)
				.WithOne(x => x.Category)
				.HasForeignKey(x => x.CategoryId);
		}
	}
}
