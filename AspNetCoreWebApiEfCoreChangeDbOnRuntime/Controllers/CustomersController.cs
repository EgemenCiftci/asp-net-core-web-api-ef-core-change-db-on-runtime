using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly MyContext _context;

    public CustomersController(MyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Customer>> GetAll(CancellationToken ct)
    {
        return await _context.Customers.ToListAsync(ct);
    }
}
