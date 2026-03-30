using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntriesController : ControllerBase
{
    private readonly PftContext _context;

    public EntriesController(PftContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Entry>> PostEntry(Entry entry)
    {
        _context.Entries.Add(entry);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEntry), new { id = entry.Id }, entry);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Entry>> GetEntry(int id)
    {
        var entry = await _context.Entries.FindAsync(id);

        if (entry == null)
        {
            return NotFound();
        }

        return entry;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetEntries()
    {
        var result = await _context.Entries.Select(x => MapModelToDto((x))).ToListAsync();
        return Ok(result);
    }

    private static Dto.Entry MapModelToDto(Models.Entry entry) =>
        new Dto.Entry
        {
            Id = entry.Id,
            Amount = entry.Amount,
            Date = entry.Date,
            Description = entry.Description,
            TypeId = entry.TypeId,
            CategoryId = entry.CategoryId,
        };
}
