namespace Report.Application.RequestModels;

public class MoveRequestModel
{
    public int FromRestId { get; set; }
    public int ToStorageId { get; set; }
    public int Quantity { get; set; }
}