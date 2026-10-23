using Chapeau.Core.Assistant;
using System.Text.Json;
using System.Net.Http.Json;
using Chapeau.Infrastructure.Data;

namespace Chapeau.Infrastructure;

public class GeminiAssistantService : IAssistantService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;

    public GeminiAssistantService(HttpClient httpClient, GeminiOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }
    
    
    public async Task<AssistantResult> GetReplyAsync(
        string message,
        IReadOnlyList<AssistantMenuItem> menu,
        CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Data",
            "menu-details.json");

        var fileJson = await File.ReadAllTextAsync(
            filePath,
            cancellationToken);

        var detailsDocument = JsonSerializer.Deserialize<MenuDetailsDocument>(
            fileJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (detailsDocument is null)
        {
            throw new InvalidOperationException(
                "Menu details could not be loaded.");
        }
        var detailsById = detailsDocument.Items
            .ToDictionary(item => item.MenuItemId);

        var enrichedMenu = menu.Select(item =>
        {
            detailsById.TryGetValue(item.Id, out var details);

            var matchingDetails = details?.Name == item.Name
                ? details
                : null;

            return new
            {
                item.Id,
                item.Name,
                item.Category,
                item.Price,
                Description = matchingDetails?.Description,
                Ingredients = matchingDetails?.Ingredients,
                Allergens = matchingDetails?.Allergens,
                AllergenNotes = matchingDetails?.AllergenNotes
            };
        }).ToList();

        string menuJson = JsonSerializer.Serialize(enrichedMenu);
        
        string instructions = $$"""
                                You are the menu assistant for Chapeau, a cosy neighbourhood bistro.
                                Your voice: a warm, food-loving server — relaxed but clean.

                                How to write:
                                - One or two short sentences, like a person talking, not a call centre.
                                - React to food ("The Sirloin is a good shout with a glass of red").
                                - Never use corporate filler: "we offer", "I suggest", "our selection".
                                - The suggested dishes appear as cards below your message — don't
                                  re-list them unless the guest asks about ingredients or allergens.
                                - Mention prices only with a dollar sign, e.g. $18.50.
                                - Point guests to their server about allergies or dietaries ONLY
                                  when they actually ask about them. Never otherwise.

                                Hard rules:
                                - Recommend only items from the supplied menu, using the supplied item IDs.
                                - Do not create or submit orders.
                                - Do not guess ingredients or allergy information.
                                
                                Ingredient and allergen rules:
                                - Answer ingredient questions using the supplied Ingredients and AllergenNotes.
                                - Missing or null information means unknown.
                                - An empty Allergens list does not mean allergen-free.
                                - Never claim a dish is allergy-safe or guarantee no cross-contact.
                                - Do not recommend items listing an allergen the guest asks to avoid.
                                - For allergy questions, explain the relevant listed information briefly
                                  and ask the guest to confirm with their server before ordering.

                                Example of the voice:
                                guest: "something light to start?" → "The Bruschetta is a fresh, light way to start."
                                guest: "does the Caesar Salad contain fish?" → "Yes, the listed dressing contains anchovies. Please confirm
                                with your server before ordering if you need to avoid fish."

                                Data status:
                                {{detailsDocument.DataStatus}}
                                
                                Allergen notice:
                                {{detailsDocument.AllergenNotice}}
                                
                                Menu:
                                {{menuJson}}
                                """;
        var requestBody = new
        {
            systemInstruction = new
            {
                parts = new[]
                {
                    new { text = instructions }
                }
            },
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = message }
                    }
                }
            },
            
            generationConfig = new
            {
                maxOutputTokens = 2048,
                responseMimeType = "application/json",
                responseSchema = new
                {
                    type = "OBJECT",
                    properties = new
                    {
                        reply = new {type = "STRING"},
                        menuItemIds = new
                        {
                            type = "ARRAY",
                            items = new{ type = "INTEGER"}
                        }
                    },
                    required = new[]{"reply","menuItemIds"}
                }
            }

        };

        using var request = new HttpRequestMessage(HttpMethod.Post, $"v1beta/models/{_options.Model}:generateContent");
        
        request.Headers.Add("x-goog-api-key", _options.ApiKey);
        
        request.Content = JsonContent.Create(requestBody);
        
        using var response = await _httpClient.SendAsync(request, cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        string responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

        using var document = JsonDocument.Parse(responseJson);

        if (!document.RootElement.TryGetProperty(
                "candidates", out var candidates) ||
            candidates.ValueKind != JsonValueKind.Array ||
            candidates.GetArrayLength() == 0)
        {
            throw new InvalidOperationException(
                "The assistant returned no answer.");
        }

        var candidate = candidates[0];
        
        if (!candidate.TryGetProperty("content", out var content) ||
            !content.TryGetProperty("parts", out var parts) ||
            parts.ValueKind != JsonValueKind.Array)
        {
            throw new InvalidOperationException(
                "The assistant returned no content.");
        }

        string answerJson = string.Empty;

        foreach (var part in parts.EnumerateArray())
        {
            
            if (part.TryGetProperty("thought", out var thought) &&
                thought.ValueKind == JsonValueKind.True)
            {
                continue;
            }

            if (part.TryGetProperty("text", out var text) &&
                text.ValueKind == JsonValueKind.String)
            {
                answerJson += text.GetString();
            }
        }

        var result = JsonSerializer.Deserialize<AssistantResult>(
            answerJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (result is null ||
            string.IsNullOrWhiteSpace(result.Reply) ||
            result.MenuItemIds is null)
        {
            throw new InvalidOperationException(
                "The assistant returned an invalid answer.");
        }

        var validMenuIds = menu
            .Select(item => item.Id)
            .ToHashSet();

        if (result.MenuItemIds.Any(id => !validMenuIds.Contains(id)))
        {
            throw new InvalidOperationException(
                "The assistant recommended an unknow menu item.");
        }

        result.MenuItemIds = result.MenuItemIds
            .Distinct()
            .ToList();

        return result;

    }
}