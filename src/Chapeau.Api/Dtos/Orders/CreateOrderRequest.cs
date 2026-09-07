namespace Chapeau.Api.Dtos.Orders;

public class CreateOrderRequest
{
    public int TableId { get; set; }
    public int EmployeeId { get; set; }
}