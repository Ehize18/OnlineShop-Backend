using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DataBase.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class GoodCountAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "GoodEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "GoodEntity");
        }
    }
}
