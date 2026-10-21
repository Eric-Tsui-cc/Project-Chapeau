using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Chapeau.Core.Assistant;
using Chapeau.Api.Dtos.Assistant;
using Microsoft.AspNetCore.RateLimiting;

namespace Chapeau.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/assistant")]
public class AssistantController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;
    private readonly IAssistantService _assistantService;

    public AssistantController(ChapeauDbContext dbContext, IAssistantService assistantService)
    {
        _dbContext = dbContext;
        _assistantService = assistantService;
    }

    [HttpPost("chat")]
    [EnableRateLimiting("assistant")]
    public async Task<ActionResult<AssistantChatResponse>> Chat(
        AssistantChatRequest request,
        CancellationToken cancellationToken)
    {
        var menu  = await _dbContext.MenuItems
            .AsNoTracking()
            .Where(item => item.Stock > 0)
            .Select(item => new AssistantMenuItem
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category.ToString(),
                Price = item.Price,
            })
            .ToListAsync(cancellationToken);

        try
        {
            var result = await _assistantService.GetReplyAsync(
                request.Message,
                menu,
                cancellationToken);

            var response = new AssistantChatResponse
            {
                Reply = result.Reply,
                MenuItemIds = result.MenuItemIds
            };
            return Ok(response);
        }
        catch (OperationCanceledException)
            when (!cancellationToken.IsCancellationRequested)
        {
            return StatusCode(StatusCodes.Status504GatewayTimeout,
                "The assistant took too long to respond. Please try again.");
        }

        catch (HttpRequestException)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                "The assistant is temporarily unavailable. Please try again later.");
        }
        catch (System.Text.Json.JsonException)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                "The assistant returned an unreadable response. Please try again.");
        }
        catch (InvalidOperationException)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                "The assistant returned an invalid response. Please try again.");
        }
    }
    
    
}