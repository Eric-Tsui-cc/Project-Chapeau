using Chapeau.Api.Dtos.Bar;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
namespace Chapeau.Api.Controllers;

[ApiController ]
[Route("api/[controller]")]
public class BarController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;
    public BarController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    private readonly MenuCategory[] _barCategories =
    {
        MenuCategory.CoffeeTea,
        MenuCategory.Beers,
        MenuCategory.Spirit,
        MenuCategory.Wines
    };

    [HttpGet("items")]
    public async Task<ActionResult<List<BarOrderItemResponse>>> GetItems()
    {
        var items = await _dbContext.OrderItems
            .Include( item => item.MenuItem)
            .Include(item => item.Order)
            .ThenInclude(order => order!.Table)
            .Where(item => item.Status == OrderStatus.Running && item.MenuItem != null && _barCategories.Contains(item.MenuItem.Category))
            .OrderBy( item => item.OrderTime)
            .AsNoTracking()
            .ToListAsync();
        
        var itemResponses = new List<BarOrderItemResponse>();
        
        foreach (OrderItem item in items)
        {   
            if (item.MenuItem is null)
            {
                return Problem("Order item has no related menu item.");
            }
            if (item.Order is null || item.Order.Table is null)
            {
                return Problem("Order item has no related order or table.");
            }
            BarOrderItemResponse response = new BarOrderItemResponse
            {
                OrderId = item.OrderId,
                OrderItemId = item.Id,
                Count = item.Count,
                OrderTime = item.OrderTime,
                Status = item.Status,
                MenuItemName = item.MenuItem.Name,
                TableNumber = item.Order.Table.Number,
                Comment =  item.Comment,
            };
            itemResponses.Add(response);
        }
        return Ok(itemResponses);
    }

    [HttpPatch("items/{id:int}/prepare")]
    public async Task<IActionResult> PrepareItem(int id)
    {
        var orderItem = await _dbContext.OrderItems
            .Include(item => item.MenuItem)
            .FirstOrDefaultAsync(item =>
                item.Id == id);
        
        if (orderItem is null)
        {
            return NotFound();
        }
        
        if (orderItem.MenuItem is null)
        {
            return Problem("Order item has no related menu item.");
        }
        if (!_barCategories.Contains(orderItem.MenuItem.Category))
        {
            return NotFound("Item is not handled by the bar.");
        }
        
        if (orderItem.Status == OrderStatus.Prepared)
        {
            return NoContent();
        }
        
        bool isAllowedTransition =
            (orderItem.Status == OrderStatus.Running);

        if (isAllowedTransition is false)
        {
            return Conflict();
        }

        orderItem.Status = OrderStatus.Prepared;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}