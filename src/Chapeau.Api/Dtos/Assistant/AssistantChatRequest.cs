using System.ComponentModel.DataAnnotations;

namespace Chapeau.Api.Dtos.Assistant;

public class AssistantChatRequest
{   
    [Required]
    [MaxLength(500)]
    public string Message { get; set; } =  string.Empty;
}