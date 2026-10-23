namespace Chapeau.Infrastructure.Data;

public class MenuItemDetails
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public List<string> Allergens { get; set; } = new();
    public string? AllergenNotes { get; set; }
}