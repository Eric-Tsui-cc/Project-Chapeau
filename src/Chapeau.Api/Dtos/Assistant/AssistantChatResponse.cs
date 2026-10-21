
namespace Chapeau.Api.Dtos.Assistant;

public class AssistantChatResponse
{
    public List<int> MenuItemIds { get; set; } = new();
    public string Reply  { get; set; } = string.Empty;
}