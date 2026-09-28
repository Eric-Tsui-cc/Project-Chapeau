using Chapeau.Core.Enums;
using System.ComponentModel.DataAnnotations;
namespace Chapeau.Api.Dtos.Bill;

public class CheckoutRequest
{
    public int OrderId { get; set; }
    
    [Required]
    public PaymentMethod? PaymentMethod { get; set; }
    public decimal? OverrideAmount { get; init; }
    public string? AdjustmentReason { get; set; } = string.Empty; 
    public decimal Tip { get; set; }
    public string? Feedback { get; set; } = string.Empty;
    
}