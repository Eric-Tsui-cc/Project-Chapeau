using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chapeau.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemUnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderItems",
                type: "TEXT",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Bills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_OrderId",
                table: "Bills",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Orders_OrderId",
                table: "Bills",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.Sql("""
                                 UPDATE OrderItems
                                 SET UnitPrice = (
                                     SELECT Price
                                     FROM MenuItems
                                     WHERE MenuItems.Id = OrderItems.MenuItemId
                                 );
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Orders_OrderId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_OrderId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Bills");
        }
    }
}
