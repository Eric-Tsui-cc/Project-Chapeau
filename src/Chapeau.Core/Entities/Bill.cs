using Chapeau.Core.Enums;

namespace Chapeau.Core.Entities;

public class Bill : BaseEntity
{   
    public int TableId { get; set; }
    public Table? Table { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public string? AdjustmentReason { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Tip { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentTime { get; set; }
    
    public string Feedback { get; set; } = string.Empty;
}