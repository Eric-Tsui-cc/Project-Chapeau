using Chapeau.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;


namespace Chapeau.Api.Controllers;

[ApiController]
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
}