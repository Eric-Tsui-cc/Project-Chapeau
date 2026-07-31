using Microsoft.EntityFrameworkCore;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;

namespace Chapeau.Infrastructure.Data;

public class ChapeauDbContext : DbContext

{
    public ChapeauDbContext(DbContextOptions<ChapeauDbContext> options) : base(options) { }
    
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Bill> Bills => Set<Bill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //for PKs
        modelBuilder.Entity<Table>().HasKey(t => t.Id);
        modelBuilder.Entity<Employee>().HasKey(e => e.Id);
        modelBuilder.Entity<Order>().HasKey(e => e.Id);
        modelBuilder.Entity<Bill>().HasKey(e => e.Id);
        modelBuilder.Entity<MenuItem>().HasKey(e => e.Id);
        modelBuilder.Entity<OrderItem>().HasKey(e => e.Id);
        
        // for FKs
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Employee)
            .WithMany()
            .HasForeignKey(o => o.EmployeeId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemId);
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Table)
            .WithMany()
            .HasForeignKey(o => o.TableId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(o => o.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);
        
        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Table)
            .WithMany()
            .HasForeignKey(b => b.TableId);
        
        //enums to string
        modelBuilder.Entity<Table>()
            .Property(t => t.Status).HasConversion<string>();
        
        modelBuilder.Entity<Employee>()
            .Property(e => e.Role).HasConversion<string>();
        modelBuilder.Entity<Employee>()
            .Property(e => e.Status).HasConversion<string>();
        
        modelBuilder.Entity<Order>()
            .Property(o => o.Status).HasConversion<string>();
        
        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Status).HasConversion<string>();

        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Card).HasConversion<string>();
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Category).HasConversion<string>();
        
        modelBuilder.Entity<Bill>()
            .Property(b => b.PaymentMethod).HasConversion<string>();
        
        //Amount Precision

        modelBuilder.Entity<Bill>()
            .Property(b => b.Amount).HasPrecision(10, 2);
        modelBuilder.Entity<Bill>()
            .Property(b => b.Tip).HasPrecision(10, 2);
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Price).HasPrecision(10, 2);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // === Tables (10) ===
        modelBuilder.Entity<Table>().HasData(
            new Table { Id = 1, Number = 1, Capacity = 2, Status = TableStatus.Free },
            new Table { Id = 2, Number = 2, Capacity = 2, Status = TableStatus.Free },
            new Table { Id = 3, Number = 3, Capacity = 4, Status = TableStatus.Free },
            new Table { Id = 4, Number = 4, Capacity = 4, Status = TableStatus.Free },
            new Table { Id = 5, Number = 5, Capacity = 4, Status = TableStatus.Free },
            new Table { Id = 6, Number = 6, Capacity = 6, Status = TableStatus.Free },
            new Table { Id = 7, Number = 7, Capacity = 6, Status = TableStatus.Free },
            new Table { Id = 8, Number = 8, Capacity = 6, Status = TableStatus.Free },
            new Table { Id = 9, Number = 9, Capacity = 8, Status = TableStatus.Free },
            new Table { Id = 10, Number = 10, Capacity = 8, Status = TableStatus.Free }
        );

        // === Employees (4) ===
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Johnson",
                Pin = 1234,
                Role = EmployeeRole.Waiter,
                Status = EmployeeStatus.Active
            },
            new Employee
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Smith",
                Pin = 2345,
                Role = EmployeeRole.Waiter,
                Status = EmployeeStatus.Active
            },
            new Employee
            {
                Id = 3,
                FirstName = "Charlie",
                LastName = "Brown",
                Pin = 3456,
                Role = EmployeeRole.Chef,
                Status = EmployeeStatus.Active
            },
            new Employee
            {
                Id = 4,
                FirstName = "Diana",
                LastName = "Ross",
                Pin = 4567,
                Role = EmployeeRole.Bartender,
                Status = EmployeeStatus.Active
            }
        );

        // === Menu Items (39) ===
        // -- Lunch (12) --
        // Starters
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, Name = "Bruschetta", Price = 9.50m, Category = MenuCategory.Starters, Card = Card.Lunch, Stock = 20 },
            new MenuItem { Id = 2, Name = "Caesar Salad", Price = 11.00m, Category = MenuCategory.Starters, Card = Card.Lunch, Stock = 20 },
            new MenuItem { Id = 3, Name = "Soup of the Day", Price = 8.00m, Category = MenuCategory.Starters, Card = Card.Lunch, Stock = 15 },
            // Mains
            new MenuItem { Id = 4, Name = "Grilled Chicken", Price = 18.50m, Category = MenuCategory.Mains, Card = Card.Lunch, Stock = 15 },
            new MenuItem { Id = 5, Name = "Beef Burger", Price = 16.00m, Category = MenuCategory.Mains, Card = Card.Lunch, Stock = 15 },
            new MenuItem { Id = 6, Name = "Fish & Chips", Price = 17.00m, Category = MenuCategory.Mains, Card = Card.Lunch, Stock = 12 },
            new MenuItem { Id = 7, Name = "Vegetarian Pasta", Price = 15.00m, Category = MenuCategory.Mains, Card = Card.Lunch, Stock = 12 },
            // Desserts
            new MenuItem { Id = 8, Name = "Tiramisu", Price = 8.50m, Category = MenuCategory.Desserts, Card = Card.Lunch, Stock = 10 },
            new MenuItem { Id = 9, Name = "Ice Cream", Price = 6.00m, Category = MenuCategory.Desserts, Card = Card.Lunch, Stock = 20 },
            // Entremet
            new MenuItem { Id = 10, Name = "Garlic Bread", Price = 5.00m, Category = MenuCategory.Entremet, Card = Card.Lunch, Stock = 25 },
            new MenuItem { Id = 11, Name = "Fries", Price = 4.50m, Category = MenuCategory.Entremet, Card = Card.Lunch, Stock = 30 },
            // CoffeeTea
            new MenuItem { Id = 12, Name = "Coffee", Price = 3.50m, Category = MenuCategory.CoffeeTea, Card = Card.Lunch, Stock = 50 }
        );

        // -- Dinner (13) --
        // Starters
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 13, Name = "Oysters Kilpatrick", Price = 16.00m, Category = MenuCategory.Starters, Card = Card.Dinner, Stock = 10 },
            new MenuItem { Id = 14, Name = "Calamari", Price = 14.00m, Category = MenuCategory.Starters, Card = Card.Dinner, Stock = 12 },
            new MenuItem { Id = 15, Name = "Pâté", Price = 13.00m, Category = MenuCategory.Starters, Card = Card.Dinner, Stock = 10 },
            // Mains
            new MenuItem { Id = 16, Name = "Ribeye Steak", Price = 32.00m, Category = MenuCategory.Mains, Card = Card.Dinner, Stock = 10 },
            new MenuItem { Id = 17, Name = "Lamb Rack", Price = 34.00m, Category = MenuCategory.Mains, Card = Card.Dinner, Stock = 8 },
            new MenuItem { Id = 18, Name = "Salmon Fillet", Price = 28.00m, Category = MenuCategory.Mains, Card = Card.Dinner, Stock = 10 },
            new MenuItem { Id = 19, Name = "Duck Confit", Price = 30.00m, Category = MenuCategory.Mains, Card = Card.Dinner, Stock = 8 },
            new MenuItem { Id = 20, Name = "Vegetable Risotto", Price = 22.00m, Category = MenuCategory.Mains, Card = Card.Dinner, Stock = 12 },
            // Desserts
            new MenuItem { Id = 21, Name = "Crème Brûlée", Price = 10.00m, Category = MenuCategory.Desserts, Card = Card.Dinner, Stock = 10 },
            new MenuItem { Id = 22, Name = "Chocolate Lava Cake", Price = 12.00m, Category = MenuCategory.Desserts, Card = Card.Dinner, Stock = 8 },
            new MenuItem { Id = 23, Name = "Cheesecake", Price = 10.00m, Category = MenuCategory.Desserts, Card = Card.Dinner, Stock = 10 },
            // Entremet
            new MenuItem { Id = 24, Name = "Truffle Fries", Price = 8.00m, Category = MenuCategory.Entremet, Card = Card.Dinner, Stock = 20 },
            // CoffeeTea
            new MenuItem { Id = 25, Name = "Espresso Martini", Price = 14.00m, Category = MenuCategory.CoffeeTea, Card = Card.Dinner, Stock = 15 }
        );

        // -- Drinks (14) --
        // Beers
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 26, Name = "House Beer", Price = 7.00m, Category = MenuCategory.Beers, Card = Card.Drinks, Stock = 50 },
            new MenuItem { Id = 27, Name = "Craft Pale Ale", Price = 9.00m, Category = MenuCategory.Beers, Card = Card.Drinks, Stock = 40 },
            new MenuItem { Id = 28, Name = "Stout", Price = 9.00m, Category = MenuCategory.Beers, Card = Card.Drinks, Stock = 30 },
            // Wines
            new MenuItem { Id = 29, Name = "Shiraz", Price = 10.00m, Category = MenuCategory.Wines, Card = Card.Drinks, Stock = 25 },
            new MenuItem { Id = 30, Name = "Chardonnay", Price = 10.00m, Category = MenuCategory.Wines, Card = Card.Drinks, Stock = 25 },
            new MenuItem { Id = 31, Name = "Sauvignon Blanc", Price = 10.00m, Category = MenuCategory.Wines, Card = Card.Drinks, Stock = 25 },
            // Spirit
            new MenuItem { Id = 32, Name = "Whiskey", Price = 12.00m, Category = MenuCategory.Spirit, Card = Card.Drinks, Stock = 30 },
            new MenuItem { Id = 33, Name = "Vodka", Price = 10.00m, Category = MenuCategory.Spirit, Card = Card.Drinks, Stock = 30 },
            new MenuItem { Id = 34, Name = "Gin", Price = 10.00m, Category = MenuCategory.Spirit, Card = Card.Drinks, Stock = 30 },
            new MenuItem { Id = 35, Name = "Rum", Price = 10.00m, Category = MenuCategory.Spirit, Card = Card.Drinks, Stock = 30 },
            // CoffeeTea
            new MenuItem { Id = 36, Name = "Cocktail - Mojito", Price = 14.00m, Category = MenuCategory.CoffeeTea, Card = Card.Drinks, Stock = 20 },
            new MenuItem { Id = 37, Name = "Cocktail - Margarita", Price = 14.00m, Category = MenuCategory.CoffeeTea, Card = Card.Drinks, Stock = 20 },
            // Entremet (snacks)
            new MenuItem { Id = 38, Name = "Nuts Mix", Price = 5.00m, Category = MenuCategory.Entremet, Card = Card.Drinks, Stock = 30 },
            new MenuItem { Id = 39, Name = "Olives", Price = 5.00m, Category = MenuCategory.Entremet, Card = Card.Drinks, Stock = 30 }
        );
    }

}