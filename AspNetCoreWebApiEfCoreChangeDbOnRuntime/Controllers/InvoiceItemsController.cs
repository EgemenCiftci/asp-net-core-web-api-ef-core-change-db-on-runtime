using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceItemsController : ControllerBase
{
    private readonly MyContext _context;

    public InvoiceItemsController(MyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<InvoiceItem>> GetAll(CancellationToken ct)
    {
        return await _context.InvoiceItems.ToListAsync(ct);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InvoiceItem>> Get(int id, CancellationToken ct)
    {
        InvoiceItem? item = await _context.InvoiceItems.SingleOrDefaultAsync(f => f.Id == id, ct);

        return item == null ? (ActionResult<InvoiceItem>)NotFound() : (ActionResult<InvoiceItem>)Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceItem>> Add(InvoiceItem invoiceItem, CancellationToken ct)
    {
        _ = _context.InvoiceItems.Add(invoiceItem);
        _ = await _context.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(Get), new { id = invoiceItem.Id }, invoiceItem);
    }
}
