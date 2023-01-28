using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoicesController : Controller
{
    private readonly MyContext _context;

    public InvoicesController(MyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Invoice>> GetAll(CancellationToken ct)
    {
        return await _context.Invoices.ToListAsync(ct);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Invoice>> Get(int id, CancellationToken ct)
    {
        Invoice? item = await _context.Invoices.SingleOrDefaultAsync(f => f.Id == id, ct);

        return item == null ? (ActionResult<Invoice>)NotFound() : (ActionResult<Invoice>)Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> Add(Invoice invoice, CancellationToken ct)
    {
        _ = _context.Invoices.Add(invoice);
        _ = await _context.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(Get), new { id = invoice.Id }, invoice);
    }
}
