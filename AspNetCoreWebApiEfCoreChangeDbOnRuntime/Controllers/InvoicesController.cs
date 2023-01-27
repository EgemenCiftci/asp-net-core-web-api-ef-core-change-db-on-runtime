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
}
