namespace Report.Application.RequestModels;

public class ProductRequestModel : BaseRequestModel
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}