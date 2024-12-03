using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.HasIndex(x => x.Email).IsUnique();
			builder.Property(x => x.Email).HasColumnType("varchar(30)");
			builder.Property(x => x.Role).HasColumnType("varchar(5)");
			builder.Property(x => x.RefreshToken).HasColumnType("text");
			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");

			builder.HasMany(x => x.Baskets)
				.WithOne(x => x.User)
				.HasForeignKey(x => x.UsertId);
		}
	}
}
