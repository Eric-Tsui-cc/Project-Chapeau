namespace Chapeau.Api.Dtos.Orders;

public class AddOrderItemRequest
{
    public int MenuItemId { get; set; }
    public int Count { get; set; }
    public string Comment { get; set; } =  string.Empty;
}