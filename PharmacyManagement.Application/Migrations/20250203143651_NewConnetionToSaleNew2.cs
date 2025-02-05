using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement.Application.Migrations
{
    /// <inheritdoc />
    public partial class NewConnetionToSaleNew2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedSales_SaleId",
                table: "ConfirmedSales",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmedSales_Sales_SaleId",
                table: "ConfirmedSales",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmedSales_Sales_SaleId",
                table: "ConfirmedSales");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmedSales_SaleId",
                table: "ConfirmedSales");
        }
    }
}
