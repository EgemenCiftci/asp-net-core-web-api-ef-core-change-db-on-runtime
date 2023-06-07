using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class MyContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ConnectionService _connectionService;

    public MyContext(DbContextOptions<MyContext> options,
                     IHttpContextAccessor httpContextAccessor,
                     IConfiguration configuration,
                     ConnectionService connectionService) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _connectionService = connectionService;
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

    private string? GetHeaderValue()
    {
        string? headerKey = _configuration["CustomHeaderKey"] ?? throw new ArgumentNullException("CustomerHeaderKey is not defined in appsettings.json file.");
        StringValues value = default;

        bool? result = _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(headerKey, out value);

        return result.GetValueOrDefault() ? value.FirstOrDefault() : null;
    }

    private string? GetConnectionString()
    {
        string? headerValue = GetHeaderValue();

        Connection? connection = _connectionService.GetConnection(headerValue) ?? _connectionService.GetDefaultConnection();

        return connection?.ConnectionString;
    }
}
