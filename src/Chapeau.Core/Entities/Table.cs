using Chapeau.Core.Enums;
namespace Chapeau.Core.Entities;

public class Table : BaseEntity 
{
    public int Capacity { get; set; }
    public TableStatus Status { get; set; }
}