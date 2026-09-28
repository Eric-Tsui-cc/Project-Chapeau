using Chapeau.Api.Dtos.Bill;
using Chapeau.Core.Entities;
using Chapeau.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;

    public BillController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("/api/bill")]
    public async Task<ActionResult<BillResponse>> CreateBill(CheckoutRequest request)
    {   
        decimal subtotal = 0;
        var order = await _dbContext.Orders
            .Include(order => order.Items)
            .Include(order => order.Table )
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order == null)
        {
            return NotFound("Could not find order");
        }
        

        if (order.Items.Count == 0)
        {
            return BadRequest("No items found");
        }

        if (order.IsPaid)
        {
            return Conflict("Bill already paid");
        }
        if (order.Table == null)
        {
            return NotFound("Could not find table");
        }
        
        if (request.OverrideAmount.HasValue)
        {
            decimal amount = request.OverrideAmount.Value;
            if (amount < 0 || amount != decimal.Round(amount,2))
            { 
                return BadRequest("Amount must be non-negative and have at most two decimal places.");
            }
        }
        
        
        if (request.Tip < 0 || request.Tip != decimal.Round(request.Tip, 2))
        {
            return BadRequest("Tip must be non-negative and have at most two decimal places.");
        }
        if (request.PaymentMethod is null)
        {
            return BadRequest("Payment method is required.");
        }
        if (!Enum.IsDefined(typeof(PaymentMethod), request.PaymentMethod.Value))
        {
            return BadRequest("Invalid payment method.");
        }

        if (order.Items.Any(item => item.Status != OrderStatus.Served))
        {
            return Conflict("Items must be served");
        }
        foreach (OrderItem item in order.Items)
        {
            subtotal = subtotal + item.UnitPrice * item.Count;
        }

        Bill bill = new Bill
        {   
            
            AdjustmentReason = request.AdjustmentReason,
            Amount = request.OverrideAmount ?? subtotal,
            Feedback = request.Feedback ?? string.Empty,
            OrderId = request.OrderId,
            PaymentMethod = request.PaymentMethod.Value,
            PaymentTime = DateTime.UtcNow,
            Table = order.Table,
            TableId = order.TableId,
            Tip = request.Tip,
        };

        order.IsPaid = true;
        order.Table.Status = TableStatus.Free;
        
        _dbContext.Bills.Add(bill);
        await _dbContext.SaveChangesAsync();

        BillResponse response = new BillResponse
        {
            AdjustmentReason = bill.AdjustmentReason ?? string.Empty,
            Amount = bill.Amount,
            Feedback = bill.Feedback,
            Id = bill.Id,
            OrderId = bill.OrderId,
            PaymentMethod = bill.PaymentMethod,
            PaymentTime = bill.PaymentTime,
            TableNumber = bill.Table.Number,
            Tip = bill.Tip,
        };
        return StatusCode(StatusCodes.Status201Created, response);

    }

}