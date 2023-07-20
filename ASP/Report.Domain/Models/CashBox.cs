﻿namespace Report.Domain.Models;

public class CashBox:BaseNetWorth
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public override string ToString()
    {
        return Name;
    }
}