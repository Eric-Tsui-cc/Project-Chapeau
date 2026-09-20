using System.ComponentModel.DataAnnotations;
using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.Kitchen;

public class UpdateKitchenItemStatusRequest
{   
    [Required]
    public OrderStatus? Status { get; init; }
}