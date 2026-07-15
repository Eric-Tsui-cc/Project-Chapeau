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
            .HasForeignKey(o => o.EmployeeID);
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Table)
            .WithMany()
            .HasForeignKey(o => o.TableID);

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
        
    }
    

}