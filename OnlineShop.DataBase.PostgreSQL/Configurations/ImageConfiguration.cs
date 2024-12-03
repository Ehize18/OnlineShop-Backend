﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.DataBase.PostgreSQL.Entities;

namespace OnlineShop.DataBase.PostgreSQL.Configurations
{
	public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
	{
		public void Configure(EntityTypeBuilder<ImageEntity> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Name).HasColumnType("text");
			builder.Property(x => x.Path).HasColumnType("text");

			builder.HasIndex(x => x.Path).IsUnique();

			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");

			builder.HasOne(x => x.Good)
				.WithMany(x => x.Images)
				.HasForeignKey(x => x.GoodId);
		}
	}
}