using Report.Application.RequestModels;
using Report.Domain.Models;

namespace Report.Application.ResponseModels;

public class ClientResponseModel:BaseNetWorth
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Income { get; set; }
    public decimal Sales { get; set; }
}