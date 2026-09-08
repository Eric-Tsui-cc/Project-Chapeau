using Chapeau.Api.Dtos.Orders;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
namespace Chapeau.Api.Controllers;

[ApiController]
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
        var table = await _dbContext.Tables
            .FirstOrDefaultAsync(table =>
                table.Id == request.TableId);
        
        if (table is null)
        {
            return NotFound();
        }

        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(employee =>
                employee.Id == request.EmployeeId);
        
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
            Status =  orderItem.Status

        };
        return StatusCode(StatusCodes.Status201Created, response);

    }

}