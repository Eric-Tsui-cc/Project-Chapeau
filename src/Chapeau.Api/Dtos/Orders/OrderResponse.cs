using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.Orders;

public class OrderResponse
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int EmployeeId { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedAt { get; set; }
}