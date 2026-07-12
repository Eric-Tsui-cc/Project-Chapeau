using Chapeau.Core.Enums;
namespace Chapeau.Core.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserCode { get; set; } =  string.Empty;
    public EmployeeRole Role { get; set; }
    public EmployeeStatus Status { get; set; }
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}