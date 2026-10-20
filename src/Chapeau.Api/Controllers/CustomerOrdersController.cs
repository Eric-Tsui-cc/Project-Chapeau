using Chapeau.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Chapeau.Api.Dtos.MenuItems;
using Chapeau.Api.Dtos.CustomerOrders;
using Chapeau.Core.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Chapeau.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/customer/orders")]
public class CustomerOrdersController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;

    public CustomerOrdersController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("items")]
    public async Task<IActionResult> SubmitItems(
        SubmitCustomerOrderRequest request)
    {
        if (request.TableNumber <= 0)
        {
            return  BadRequest("wrong table number");
        }

        if (request.Items is null || request.Items.Count == 0)
        {
            return BadRequest("Items are required.");
        }

        foreach (var item in request.Items)
        {
            if (item is null)
            {
                return BadRequest("Items must not contain null entries.");
            }
            if (item.MenuItemId <= 0 || item.Count <= 0)
            {
                return BadRequest("Menu item ID and quantity must be greater than zero.");
            }
        }

        var table = await _dbContext.Tables
            .SingleOrDefaultAsync(table => table.Number == request.TableNumber);

        if (table is null)
        {
            return NotFound("Table not found");
        }

        var order = await _dbContext.Orders
            .Include(order => order.Items)
            .SingleOrDefaultAsync(order => order.TableId == table.Id && order.IsPaid == false);
        if (order is null)
        {
            return Conflict("Plase ask service for ordering");
        }

        if (order.Items.Count > 0)
        {
            return Conflict("An order has already been placed. Please ask your server to add items.");
        }

        var requiredCounts = new Dictionary<int, int>();

        foreach (var item in request.Items)
        {
            requiredCounts.TryGetValue(item.MenuItemId, out int existingCount);

            if (item.Count > int.MaxValue - existingCount)
            {
                return BadRequest("Total quantity is too large.");
            }

            requiredCounts[item.MenuItemId] = existingCount + item.Count;
        }

        var menuItemIds = requiredCounts.Keys.ToList();

        var menuItems = await _dbContext.MenuItems
            .Where(item => menuItemIds.Contains(item.Id))
            .ToListAsync();
        if (menuItems.Count < requiredCounts.Count)
        {
            return NotFound("One or more menu items were not found.");
        }

        foreach (var menuItem in menuItems)
        {
            var requiredCount = requiredCounts[menuItem.Id];
            if (menuItem.Stock < requiredCount)
            {
                return Conflict("Not enough stock available.");
            }
        }

        DateTime OrderTime = DateTime.UtcNow;

        foreach (var item1 in request.Items)
        {
            OrderItem orderItem = new OrderItem();
            var menuItem = menuItems.First(menu => menu.Id == item1.MenuItemId);
            orderItem.OrderId = order.Id;
            orderItem.OrderTime = OrderTime;
            orderItem.MenuItemId = menuItem.Id;
            orderItem.Count = item1.Count;
            orderItem.Comment = item1.Comment ?? string.Empty;
            orderItem.UnitPrice = menuItem.Price;
            orderItem.Status = OrderStatus.Running;

            _dbContext.OrderItems.Add(orderItem);
        }

        foreach (var menuItem in menuItems)
        {
            menuItem.Stock -= requiredCounts[menuItem.Id];
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new
        {
            orderId = order.Id,
            tableNumber = table.Number,
            message = "Your order has been submitted."
        });
    }

}