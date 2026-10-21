namespace Chapeau.Core.Assistant;

public interface IAssistantService
{
    Task<AssistantResult> GetReplyAsync(
        string message,
        IReadOnlyList<AssistantMenuItem> menu,
        CancellationToken cancellationToken = default);
}