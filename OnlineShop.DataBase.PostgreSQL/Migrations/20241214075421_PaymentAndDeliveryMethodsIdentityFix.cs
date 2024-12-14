using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DataBase.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class PaymentAndDeliveryMethodsIdentityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Title",
                table: "PaymentMethods",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMethods_Title",
                table: "DeliveryMethods",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_Title",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryMethods_Title",
                table: "DeliveryMethods");
        }
    }
}
