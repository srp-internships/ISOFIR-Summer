namespace Report.Application.ResponseModels;

public class ProductResponseModel : BaseResponseModel
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Category { get; set; } = string.Empty;
}