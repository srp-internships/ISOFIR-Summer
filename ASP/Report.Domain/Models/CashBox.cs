namespace Report.Core.Models;

public class CashBox:BaseNetWorth
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
}