using Microsoft.EntityFrameworkCore;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;

namespace Chapeau.Infrastructure.Data;

public class ChapeauDbContext : DbContext

{
    public ChapeauDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Bill> Bills => Set<Bill>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    

}