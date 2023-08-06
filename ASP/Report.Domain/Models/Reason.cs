namespace Report.Domain.Models;

public class Reason : BaseEntitiesNetWorth
{
    public string Name { get; set; } = "";
    public IEnumerable<ReasonCashLog>? ReasonCashLogs { get; set; }

    public override string ToString()
    {
        return Name;
    }
}