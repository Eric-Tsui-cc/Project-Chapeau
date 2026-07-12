using Chapeau.Core.Enums;
namespace Chapeau.Core.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order?Order { get; set; }
    
    public int MenuItemId { get; set; }
    public MenuItem?MenuItem { get; set; }
    
    public int Count { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderTime { get; set; }
    public string Comment { get; set; } =  string.Empty;
    
}