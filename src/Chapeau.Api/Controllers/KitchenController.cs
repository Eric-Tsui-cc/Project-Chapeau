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

    [HttpGet("items")]
    public async Task<ActionResult<List<KitchenOrderItemResponse>>> GetItems()
    {   
        
        var kitchenCategories = new[]
        {
            MenuCategory.Starters,
            MenuCategory.Mains,
            MenuCategory.Entremet,
            MenuCategory.Desserts
        };
        
        var items = await _dbContext.OrderItems
            .Include(item => item.MenuItem)
            .Include(item => item.Order)
            .ThenInclude(order => order!.Table)
            .Where(item => (item.Status == OrderStatus.Running || item.Status == OrderStatus.Preparing) && item.MenuItem != null && kitchenCategories.Contains(item.MenuItem.Category))
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
}