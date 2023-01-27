using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class MyContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ConfigurationManager _configurationManager;

    public MyContext(DbContextOptions<MyContext> options, IHttpContextAccessor httpContextAccessor, ConfigurationManager configurationManager) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _configurationManager = configurationManager;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionName;
        StringValues value = default;

        bool? result = _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(CustomHeaderSwaggerAttribute.HeaderKey, out value);

        connectionName = result.GetValueOrDefault() ? value.FirstOrDefault() : default(ConnectionNames).ToString();

        string? connectionString = _configurationManager[$"ConnectionStrings:{connectionName}"];
        _ = optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Customer>().ToTable("Customers");
        _ = modelBuilder.Entity<Invoice>().ToTable("Invoices");
        _ = modelBuilder.Entity<InvoiceItem>().ToTable("InvoiceItems");
    }
}
