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

    [HttpGet("{id:int}")]
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
        [FromQuery] QueryFilter filter
    )
    {
        var query = _context.Entries.AsNoTracking().AsQueryable();

        // TODO Apply search filter(filter by date, description ...)
        // if (!String.IsNullOrWhiteSpace(filter.OverviewPeriod))
        // {
        //     DateTime firstDayOfWeek = DateTime.UtcNow;
        //     DateTime lastDayOfWeek = DateTime.UtcNow;
        //
        //     if (filter.OverviewPeriod == "week")
        //     {
        //         DateTime date = DateTime.UtcNow;
        //         int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        //         firstDayOfWeek = date.Date.AddDays(-diff);
        //         lastDayOfWeek = firstDayOfWeek.AddDays(6);
        //     }
        //     query = query.Where(op => op.Date >= firstDayOfWeek && op.Date < lastDayOfWeek);
        // }

        // Count total items AFTER filtering but BEFORE filter
        var totalRecords = await query.CountAsync();

        // TODO ApplySort

        var entries = await query
            .ApplyPagination(filter.PageNumber, filter.PageSize)
            .Select(x => MapModelToDto((x)))
            .ToListAsync();

        var paginatedResponse = new PaginatedResponse<Dto.Entry>
        {
            Data = entries,
            Metadata = new PaginationMetadata(filter.PageNumber, filter.PageSize, totalRecords),
        };

        return Ok(paginatedResponse);
    }

    private static Dto.Entry MapModelToDto(Entry entry) =>
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
