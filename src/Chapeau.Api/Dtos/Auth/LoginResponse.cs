using Chapeau.Core.Enums;

namespace Chapeau.Api.Dtos.Auth;

public class LoginResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
}