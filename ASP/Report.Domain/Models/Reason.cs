namespace Report.Domain.Models;

public class Reason:BaseNetWorth
{
    public string Name { get; set; } = "";
    public IEnumerable<ReasonCashLog>? ReasonCashLogs { get; set; }
    public override string ToString()
    {
        return Name;
    }
}