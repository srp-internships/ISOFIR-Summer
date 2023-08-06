namespace Report.Application.RequestModels;

public class ReasonRequestModel : BaseRequestModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}