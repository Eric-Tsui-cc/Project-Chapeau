using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.Orders;

public class OrderDetailsResponse
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public int EmployeeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TableId { get; set; }
    public List<OrderItemResponse> Items { get; set; } = new();
    public bool IsPaid { get; set; }
}