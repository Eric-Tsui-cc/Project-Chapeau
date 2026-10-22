using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chapeau.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Pin = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Card = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    AdjustmentReason = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Tip = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Pin", "Role", "Status" },
                values: new object[,]
                {
                    { 1, "Alice", "Johnson", 1234, "Waiter", "Active" },
                    { 2, "Bob", "Smith", 2345, "Waiter", "Active" },
                    { 3, "Charlie", "Brown", 3456, "Chef", "Active" },
                    { 4, "Diana", "Ross", 4567, "Bartender", "Active" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Card", "Category", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Lunch", "Starters", "Bruschetta", 9.50m, 20 },
                    { 2, "Lunch", "Starters", "Caesar Salad", 11.00m, 20 },
                    { 3, "Lunch", "Starters", "Soup of the Day", 8.00m, 15 },
                    { 4, "Lunch", "Mains", "Grilled Chicken", 18.50m, 15 },
                    { 5, "Lunch", "Mains", "Beef Burger", 16.00m, 15 },
                    { 6, "Lunch", "Mains", "Fish & Chips", 17.00m, 12 },
                    { 7, "Lunch", "Mains", "Vegetarian Pasta", 15.00m, 12 },
                    { 8, "Lunch", "Desserts", "Tiramisu", 8.50m, 10 },
                    { 9, "Lunch", "Desserts", "Ice Cream", 6.00m, 20 },
                    { 10, "Lunch", "Entremet", "Garlic Bread", 5.00m, 25 },
                    { 11, "Lunch", "Entremet", "Fries", 4.50m, 30 },
                    { 12, "Lunch", "CoffeeTea", "Coffee", 3.50m, 50 },
                    { 13, "Dinner", "Starters", "Oysters Kilpatrick", 16.00m, 10 },
                    { 14, "Dinner", "Starters", "Calamari", 14.00m, 12 },
                    { 15, "Dinner", "Starters", "Pâté", 13.00m, 10 },
                    { 16, "Dinner", "Mains", "Ribeye Steak", 32.00m, 10 },
                    { 17, "Dinner", "Mains", "Lamb Rack", 34.00m, 8 },
                    { 18, "Dinner", "Mains", "Salmon Fillet", 28.00m, 10 },
                    { 19, "Dinner", "Mains", "Duck Confit", 30.00m, 8 },
                    { 20, "Dinner", "Mains", "Vegetable Risotto", 22.00m, 12 },
                    { 21, "Dinner", "Desserts", "Crème Brûlée", 10.00m, 10 },
                    { 22, "Dinner", "Desserts", "Chocolate Lava Cake", 12.00m, 8 },
                    { 23, "Dinner", "Desserts", "Cheesecake", 10.00m, 10 },
                    { 24, "Dinner", "Entremet", "Truffle Fries", 8.00m, 20 },
                    { 25, "Dinner", "CoffeeTea", "Espresso Martini", 14.00m, 15 },
                    { 26, "Drinks", "Beers", "House Beer", 7.00m, 50 },
                    { 27, "Drinks", "Beers", "Craft Pale Ale", 9.00m, 40 },
                    { 28, "Drinks", "Beers", "Stout", 9.00m, 30 },
                    { 29, "Drinks", "Wines", "Shiraz", 10.00m, 25 },
                    { 30, "Drinks", "Wines", "Chardonnay", 10.00m, 25 },
                    { 31, "Drinks", "Wines", "Sauvignon Blanc", 10.00m, 25 },
                    { 32, "Drinks", "Spirit", "Whiskey", 12.00m, 30 },
                    { 33, "Drinks", "Spirit", "Vodka", 10.00m, 30 },
                    { 34, "Drinks", "Spirit", "Gin", 10.00m, 30 },
                    { 35, "Drinks", "Spirit", "Rum", 10.00m, 30 },
                    { 36, "Drinks", "CoffeeTea", "Cocktail - Mojito", 14.00m, 20 },
                    { 37, "Drinks", "CoffeeTea", "Cocktail - Margarita", 14.00m, 20 },
                    { 38, "Drinks", "Entremet", "Nuts Mix", 5.00m, 30 },
                    { 39, "Drinks", "Entremet", "Olives", 5.00m, 30 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Capacity", "Number", "Status" },
                values: new object[,]
                {
                    { 1, 2, 1, "Free" },
                    { 2, 2, 2, "Free" },
                    { 3, 4, 3, "Free" },
                    { 4, 4, 4, "Free" },
                    { 5, 4, 5, "Free" },
                    { 6, 6, 6, "Free" },
                    { 7, 6, 7, "Free" },
                    { 8, 6, 8, "Free" },
                    { 9, 8, 9, "Free" },
                    { 10, 8, 10, "Free" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_OrderId",
                table: "Bills",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TableId",
                table: "Bills",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Pin",
                table: "Employees",
                column: "Pin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_Number",
                table: "Tables",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
