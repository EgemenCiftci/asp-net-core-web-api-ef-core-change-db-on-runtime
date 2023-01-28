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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Customer>> Get(int id, CancellationToken ct)
    {
        Customer? item = await _context.Customers.SingleOrDefaultAsync(f => f.Id == id, ct);

        return item == null ? (ActionResult<Customer>)NotFound() : (ActionResult<Customer>)Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Add(Customer customer, CancellationToken ct)
    {
        _ = _context.Customers.Add(customer);
        _ = await _context.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
    }
}
