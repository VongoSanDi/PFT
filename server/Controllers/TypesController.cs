using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypesController : ControllerBase
{
    private readonly PftContext _context;

    public TypesController(PftContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dto.Type>>> GetTypes()
    {
        var result = await _context.Types.Select(x => MapModelToDto(x)).ToListAsync();
        return Ok(result);
    }

    private static Dto.Type MapModelToDto(Models.Type type) =>
        new Dto.Type
        {
            Id = type.Id,
            Name = type.Name,
            Description = type.Description,
        };
}
