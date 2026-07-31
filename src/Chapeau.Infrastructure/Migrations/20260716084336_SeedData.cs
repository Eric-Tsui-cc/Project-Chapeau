using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chapeau.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Tables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Role", "Status", "UserCode" },
                values: new object[,]
                {
                    { 1, "Alice", "Johnson", "Waiter", "Active", "A001" },
                    { 2, "Bob", "Smith", "Waiter", "Active", "A002" },
                    { 3, "Charlie", "Brown", "Chef", "Active", "B001" },
                    { 4, "Diana", "Ross", "Bartender", "Active", "C001" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tables");
        }
    }
}
