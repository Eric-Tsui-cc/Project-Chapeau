using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Chapeau.Api.Dtos.Auth;
using Chapeau.Core.Enums;
using System.Security.Claims;
using Chapeau.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Antiforgery;

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

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
            new Claim(ClaimTypes.Role, employee.Role.ToString())
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);




        var response = new LoginResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role,
        };
        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<LoginResponse>> Me()
    {
        var employeeIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(employeeIdText, out int employeeId))
        {
            return Unauthorized();
        }

        var employee = await _dbContext.Employees
            .Where(e => e.Status == EmployeeStatus.Active)
            .FirstOrDefaultAsync(employee => employee.Id == employeeId);

        if (employee is null)
        {
            return Unauthorized();
        }

        LoginResponse response = new LoginResponse
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Id = employee.Id,
            Role = employee.Role,
        };

        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("csrf")]
    public IActionResult GetCsrfToken(
        [FromServices] IAntiforgery antiforgery)
    {
        var tokens = antiforgery.GetAndStoreTokens(HttpContext);
        return Ok(new
        {
            requestToken = tokens.RequestToken
        });
    }
}
