namespace Report.Application.RequestModels;

public class RestFilterRequestModel : BaseRequestModel
{
    public int ProductId { get; set; }
    public int StorageId { get; set; }
    public int FirmId { get; set; }
}