namespace Chapeau.Api.Dtos.CustomerOrders;

public class CustomerOrderItemRequest
{
    public int MenuItemId { get; set; }
    public int Count { get; set; }
    public string Comment { get; set; } =  string.Empty;

}