using Chapeau.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Chapeau.Api.Dtos.MenuItems;



namespace Chapeau.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;

    public MenuItemsController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<ActionResult<List<MenuItem>>> GetAll()
    {
        var items = await _dbContext.MenuItems
            .OrderBy(item => item.Card)
            .ThenBy(item => item.Category)
            .ThenBy(item => item.Name)
            .ToListAsync();
        return Ok(items);
    }

    [AllowAnonymous]
    [HttpGet("customer")]
    public async Task<ActionResult<List<MenuItemResponse>>> GetCustomerMenu()
    {
        var items = await _dbContext.MenuItems
            .AsNoTracking()
            .OrderBy(item => item.Card)
            .ThenBy(item => item.Category)
            .ThenBy(item => item.Name)
            .ToListAsync();

        List<MenuItemResponse> responses = new List<MenuItemResponse>();

        foreach (var item in  items)
        {
            MenuItemResponse response = new MenuItemResponse
            {
                Card = item.Card,
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                IsAvailable = item.Stock > 0,
                Price =  item.Price,
            };
            responses.Add(response);
        }
        return Ok(responses);
    }
}