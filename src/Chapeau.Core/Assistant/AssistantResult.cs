namespace Chapeau.Core.Assistant;

public class AssistantResult
{
    public string Reply { get; set; } = string.Empty;
    public List<int> MenuItemIds { get; set; } = new();
}