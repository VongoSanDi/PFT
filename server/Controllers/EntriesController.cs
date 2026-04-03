using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Common;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntriesController : ControllerBase
{
    private readonly PftContext _context;
    private readonly ILogger<EntriesController> _logger;

    public EntriesController(PftContext context, ILogger<EntriesController> logger)
    {
        _context = context;
        _logger = logger;
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
    public async Task<ActionResult<IEnumerable<Category>>> GetEntries(
        [FromQuery] QueryFilter pagination
    )
    {
        // Console.WriteLine("page", pagination);
        _logger.LogInformation("page");
        var query = _context.Entries.AsNoTracking().AsQueryable();

        // TODO Apply search filter(filter by date, description ...)

        // Count total items AFTER filtering but BEFORE pagination
        var totalRecords = await query.CountAsync();

        // TODO ApplySort

        var entries = await _context
            .Entries.ApplyPagination(pagination.PageNumber, pagination.PageSize)
            .Select(x => MapModelToDto((x)))
            .ToListAsync();

        var paginatedResponse = new PaginatedResponse<Dto.Entry>
        {
            Data = entries,
            Metadata = new PaginationMetadata(
                pagination.PageNumber,
                pagination.PageSize,
                totalRecords
            ),
        };

        return Ok(paginatedResponse);
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
