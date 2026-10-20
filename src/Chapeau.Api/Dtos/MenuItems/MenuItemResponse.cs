using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.MenuItems;

public class MenuItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MenuCategory Category { get; set; }
    public Card Card { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}