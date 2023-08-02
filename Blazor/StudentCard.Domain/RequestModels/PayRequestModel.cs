namespace StudentCard.Domain.RequestModels;

public class PayRequestModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public decimal Sum { get; set; }
    public int AgentId { get; set; }
}