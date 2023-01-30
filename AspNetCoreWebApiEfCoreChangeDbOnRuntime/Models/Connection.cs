namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class Connection
{
    public string? Name { get; set; }

    public string? Label { get; set; }

    public bool IsDefault { get; set; }

    public string? ConnectionString { get; set; }
}
