namespace Chapeau.Infrastructure.Data;

public class MenuDetailsDocument
{
    public string DataStatus { get; set; } = string.Empty;
    public string AllergenNotice { get; set; } = string.Empty;
    public List<MenuItemDetails> Items { get; set; } = new();
}