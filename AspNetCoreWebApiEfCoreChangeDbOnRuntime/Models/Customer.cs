﻿namespace AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}

