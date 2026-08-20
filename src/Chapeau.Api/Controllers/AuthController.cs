using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Chapeau.Api.Dtos.Auth;
using Chapeau.Core.Enums;


namespace Chapeau.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;

    public AuthController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(employee => employee.Pin == request.Pin && employee.Status == EmployeeStatus.Active);
        if (employee is null)
        {
            return Unauthorized(new { Message = "Invalid PIN" });
        }

        var response = new LoginResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role,
        };
        return Ok(response);
    }
}