using Chapeau.Core.Enums;

namespace Chapeau.Core.Entities;

public class Bill : BaseEntity
{   
    public int TableId { get; set; }
    public Table? Table { get; set; }
    
    public decimal Amount { get; set; }
    public decimal Tip { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentTime { get; set; }
    
    public string Feedback { get; set; } = string.Empty;
}