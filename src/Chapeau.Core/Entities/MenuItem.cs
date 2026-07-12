using Chapeau.Core.Enums;
namespace Chapeau.Core.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public MenuCategory Category { get; set; }
    public Card Card { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}