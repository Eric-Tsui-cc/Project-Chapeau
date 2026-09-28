using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.Bill;

public class BillResponse
{   
    public int Id { get; set; }
    public int OrderId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentTime { get; set; }
    public int TableNumber { get; set; }
    public string? AdjustmentReason { get; set; }
    public decimal Amount { get; set; }
    public decimal Tip { get; set; }
    public string? Feedback { get; set; }
    
}