namespace Chapeau.Api.Dtos.CustomerOrders;

public class SubmitCustomerOrderRequest
{
    public int TableNumber { get; set; }
    public List<CustomerOrderItemRequest> Items { get; set; } = new();
}