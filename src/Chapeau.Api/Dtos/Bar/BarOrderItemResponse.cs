using Chapeau.Core.Enums;
namespace Chapeau.Api.Dtos.Bar;

public class BarOrderItemResponse
{
    public int OrderId { get; set; }
    public int OrderItemId { get; set; }
    public int TableNumber { get; set; }
    public int Count { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderTime{ get; set; }
    public string MenuItemName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;

}