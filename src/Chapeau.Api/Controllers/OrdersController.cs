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
}