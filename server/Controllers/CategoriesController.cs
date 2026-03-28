using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly PftContext _context;

    public CategoriesController(PftContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dto.Type>>> GetTypes()
    {
        var result = await _context.Categories.Select(x => MapModelToDto(x)).ToListAsync();
        return Ok(result);
    }

    private static Dto.Category MapModelToDto(Models.Category category) =>
        new Dto.Category
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
}
