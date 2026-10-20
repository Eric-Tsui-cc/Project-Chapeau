using Chapeau.Api.Dtos.Orders;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Chapeau.Api.Controllers;

[ApiController]
[Authorize(Roles = "Waiter")]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;
    public OrdersController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(CreateOrderRequest request)
    {
        var employeeIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(employeeIdText, out int employeeId))
        {
            return Unauthorized();
        }

        var table = await _dbContext.Tables
            .FirstOrDefaultAsync(table =>
                table.Id == request.TableId);
        
        if (table is null)
        {
            return NotFound();
        }

        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(employee =>
                employee.Id == employeeId);
        
        if (employee is null )
        {
            return NotFound();
        }

        if (employee.Status == EmployeeStatus.Inactive)
        {
            return BadRequest();
        }

        var hasOpenOrder = await _dbContext.Orders
            .AnyAsync(order =>
                order.TableId == request.TableId &&
                order.IsPaid == false);

        if (hasOpenOrder)
        {
            return Conflict();
        }

        var order = new Order
        {
            Status = OrderStatus.Running,
            IsPaid = false,
            EmployeeId = employee.Id,
            TableId = table.Id,
            CreatedAt = DateTime.UtcNow
        };

        table.Status = TableStatus.Occupied;
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        OrderResponse orderResponse = new OrderResponse
        {   
            Id =  order.Id,
            Status = order.Status,
            IsPaid = order.IsPaid,
            EmployeeId = order.EmployeeId,
            TableId = order.TableId,
            CreatedAt = order.CreatedAt
        };
        return StatusCode(StatusCodes.Status201Created, orderResponse);

    }

    
    [HttpPost("{orderId:int}/items")]
    public async Task<ActionResult<OrderItemResponse>> AddItem(int orderId, AddOrderItemRequest request)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(order => order.Id == orderId);
        if (order is null)
        {
            return NotFound();
        }

        if (order.IsPaid)
        {
            return Conflict();
        }

        if (request.Count < 1)
        {
            return BadRequest();
        }

        var item = await _dbContext.MenuItems
            .FirstOrDefaultAsync(menuItem => menuItem.Id == request.MenuItemId);
        if (item is null)
        {
            return NotFound();
        }

        if (item.Stock < request.Count)
        {
            return Conflict();
        }

        OrderItem orderItem = new OrderItem
        {
            OrderId = order.Id,
            Status = OrderStatus.Running,
            OrderTime = DateTime.UtcNow,
            Count = request.Count,
            Comment = request.Comment,
            MenuItem = item,
            Order = order,
            UnitPrice = item.Price,
            MenuItemId = item.Id
        };
        
        item.Stock -= request.Count;

        
        _dbContext.OrderItems.Add(orderItem);
        await _dbContext.SaveChangesAsync();

        OrderItemResponse response = new OrderItemResponse
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            MenuItemId = orderItem.MenuItemId,
            Count = orderItem.Count,
            Comment = orderItem.Comment,
            OrderTime = orderItem.OrderTime,
            MenuItemName = item.Name,
            Status =  orderItem.Status,
            UnitPrice = orderItem.UnitPrice

        };
        return StatusCode(StatusCodes.Status201Created, response);

    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDetailsResponse>> GetById(int id)
    {
        var order = await _dbContext.Orders
            .Include(order => order.Items )
            .ThenInclude(item => item.MenuItem)
            .FirstOrDefaultAsync(order => order.Id == id);
        if (order is null)
        {
            return NotFound();
        }

        OrderDetailsResponse detailsResponse = new OrderDetailsResponse
        {
            Id = order.Id,
            Status = order.Status,
            EmployeeId = order.EmployeeId,
            TableId = order.TableId,
            CreatedAt = order.CreatedAt,
            IsPaid = order.IsPaid
        };

        foreach (OrderItem item in order.Items)
        {   
            if (item.MenuItem is null)
            {
                return Problem("Order item has no related menu item.");
            }
            OrderItemResponse orderItemResponse = new OrderItemResponse
            {
                Id = item.Id,
                OrderId = item.OrderId,
                MenuItemId = item.MenuItemId,
                Count = item.Count,
                Comment = item.Comment,
                OrderTime = item.OrderTime,
                MenuItemName = item.MenuItem.Name,
                Status = item.Status,
                UnitPrice = item.UnitPrice
            };
            detailsResponse.Items.Add(orderItemResponse);
        }
        return Ok(detailsResponse);
    }

    [HttpPatch("{orderId:int}/items/{itemId:int}/serve")]
    public async Task<IActionResult> ServeItem(int orderId, int itemId)
    {
        var orderItem = await _dbContext.OrderItems
            .FirstOrDefaultAsync(item => 
                item.Id == itemId && item.OrderId == orderId);
        
        if (orderItem is null)
        {
            return NotFound();
        }

        if (orderItem.Status == OrderStatus.Prepared)
        {
            orderItem.Status = OrderStatus.Served;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        
        if (orderItem.Status == OrderStatus.Served)
        {
            return NoContent();
        }

        return Conflict();
    }

    [HttpGet("current")]
    public async Task<ActionResult<OrderResponse>> GetCurrentByTable(
        [FromQuery] int tableId)
    {
        var order = await _dbContext.Orders
            .AsNoTracking()
            .SingleOrDefaultAsync(order =>
                order.TableId == tableId && !order.IsPaid);

        if (order is null)
        {
            return NotFound("Order not found");
        }

        OrderResponse response = new OrderResponse()
        {
            CreatedAt = order.CreatedAt,
            IsPaid = order.IsPaid,
            TableId = order.TableId,
            Status = order.Status,
            EmployeeId = order.EmployeeId,
            Id = order.Id,
        };
        
        return Ok(response);
        
    }
    

}