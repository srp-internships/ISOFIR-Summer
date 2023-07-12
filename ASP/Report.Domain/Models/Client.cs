    namespace Report.Core.Models;

public class Client:BaseNetWorth
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public decimal Income { get; set; }
    public decimal CashTjs { get; set; }
    public decimal CashUsd { get; set; }
}