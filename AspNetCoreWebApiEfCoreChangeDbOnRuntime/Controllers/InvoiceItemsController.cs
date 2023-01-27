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
}
