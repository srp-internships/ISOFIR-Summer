using Report.Domain.Models;

namespace Report.Application.RequestModels;

public class ReasonCashBoxRequestModel : BaseEntitiesNetWorth
{
    public int ReasonId { get; set; }
    public DateTime DateTime { get; set; }
    public int CashBoxId { get; set; }
}