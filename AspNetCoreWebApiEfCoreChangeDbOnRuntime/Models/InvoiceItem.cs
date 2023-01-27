namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class InvoiceItem
{
    public int InvoiceItemId { get; set; }
    public int InvoiceId { get; set; }
    public string? Code { get; set; }
    public Invoice? Invoice { get; set; }
}
