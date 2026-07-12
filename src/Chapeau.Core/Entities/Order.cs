using Chapeau.Core.Enums;
namespace Chapeau.Core.Entities;

public class Order : BaseEntity
{
    public int TableID { get; set; }
    public Table?Table { get; set; }
    
    public int EmployeeID { get; set; }
    public Employee? Employee { get; set; }
    
    public OrderStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    

}