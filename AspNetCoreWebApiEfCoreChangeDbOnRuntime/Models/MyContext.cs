using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class MyContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ConfigurationManager _configurationManager;

    public MyContext(DbContextOptions<MyContext> options,
                     IHttpContextAccessor httpContextAccessor,
                     ConfigurationManager configurationManager) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _configurationManager = configurationManager;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.UseSqlite(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Customer>().ToTable("Customers");
        _ = modelBuilder.Entity<Invoice>().ToTable("Invoices");
        _ = modelBuilder.Entity<InvoiceItem>().ToTable("InvoiceItems");
    }

    private string? GetConnectionString()
    {
        string? headerKey = _configurationManager["CustomHeaderKey"];

        if (headerKey == null)
        {
            throw new ArgumentNullException("CustomerHeaderKey is not defined in appsettings.json file.");
        }

        StringValues value = default;

        bool? result = _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(headerKey, out value);

        string? connectionName = result.GetValueOrDefault() ? value.FirstOrDefault() : default(ConnectionNames).ToString();

        return _configurationManager[$"ConnectionStrings:{connectionName}"];
    }
}
