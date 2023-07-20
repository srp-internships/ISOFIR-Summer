using Report.Domain.Models;

namespace Report.Application.ResponseModels;

public class CashBoxResponseModel:BaseNetWorth
{
    public string Name { get; set; }
}