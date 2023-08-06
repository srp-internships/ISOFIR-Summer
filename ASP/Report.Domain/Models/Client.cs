namespace Report.Domain.Models;

public class Client : BaseEntitiesNetWorth
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public decimal Income { get; set; }
    public decimal Sales { get; set; }

    public override string ToString()
    {
        return Name;
    }
}