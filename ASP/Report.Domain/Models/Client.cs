namespace Report.Domain.Models;

public class Client : BaseNetWorth
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public decimal Income { get; set; }
    public override string ToString()
    {
        return Name;
    }
}