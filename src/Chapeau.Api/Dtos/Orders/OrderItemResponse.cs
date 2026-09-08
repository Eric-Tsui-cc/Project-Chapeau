using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.Orders;

public class OrderItemResponse
{
    public int Id { get; set; }
    public int  OrderId { get; set; }
    public string MenuItemName { get; set; } =  string.Empty;
    public DateTime OrderTime{ get; set; }
    public int MenuItemId { get; set; }
    public int Count { get; set; }
    public OrderStatus Status { get; set; }
    public string Comment { get; set; } = string.Empty;
}