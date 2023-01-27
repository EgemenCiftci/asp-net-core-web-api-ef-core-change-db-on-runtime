namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class Invoice
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<InvoiceItem>? Items { get; set; }
}
