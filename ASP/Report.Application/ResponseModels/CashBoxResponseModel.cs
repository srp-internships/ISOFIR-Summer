using Report.Domain.Models;

namespace Report.Application.ResponseModels;

public class CashBoxResponseModel : BaseEntitiesNetWorth
{
    public string Name { get; set; }
}