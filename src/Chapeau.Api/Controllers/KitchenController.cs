using Chapeau.Api.Dtos.Kitchen;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KitchenController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;
    public KitchenController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly MenuCategory[] _kitchenCategories =
    {
        MenuCategory.Mains,
        MenuCategory.Entremet,
        MenuCategory.Desserts,
        MenuCategory.Starters
    };
    
    [HttpGet("items")]
    public async Task<ActionResult<List<KitchenOrderItemResponse>>> GetItems()
    {   
        

        var items = await _dbContext.OrderItems
            .Include(item => item.MenuItem)
            .Include(item => item.Order)
            .ThenInclude(order => order!.Table)
            .Where(item => (item.Status == OrderStatus.Running || item.Status == OrderStatus.Preparing) && item.MenuItem != null && _kitchenCategories.Contains(item.MenuItem.Category))
            .OrderBy(t => t.OrderTime)
            .AsNoTracking()
            .ToListAsync();
        
        var itemResponses = new List<KitchenOrderItemResponse>();
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
            KitchenOrderItemResponse response = new KitchenOrderItemResponse
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

    [HttpPatch("items/{id:int}/status")]
    public async Task<IActionResult> UpdateItemStatus(
        int id,
        UpdateKitchenItemStatusRequest request)
    {   

        if (request.Status != OrderStatus.Preparing && request.Status != OrderStatus.Prepared)
        {
            return BadRequest();
        }

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
        if (!_kitchenCategories.Contains(orderItem.MenuItem.Category))
        {
            return NotFound("Item is not handled by the kitchen.");
        }

        if (orderItem.Status == request.Status)
        {
            return NoContent();
        }
        bool isAllowedTransition =
            (orderItem.Status == OrderStatus.Running && request.Status == OrderStatus.Preparing) ||
            (orderItem.Status == OrderStatus.Preparing && request.Status == OrderStatus.Prepared);
        

        if (isAllowedTransition is false)
        {
            return Conflict();
        }
        orderItem.Status = request.Status.Value;
        await _dbContext.SaveChangesAsync();
        return NoContent();
        
    }
}