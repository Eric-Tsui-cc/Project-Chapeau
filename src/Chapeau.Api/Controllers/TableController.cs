using Chapeau.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Chapeau.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;


namespace Chapeau.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly ChapeauDbContext _dbContext;
    public TablesController(ChapeauDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Table>>> GetAll()
    {
        var tables = await _dbContext.Tables
            .OrderBy(t => t.Number)
            .ToListAsync();
        return Ok(tables);
    }
}