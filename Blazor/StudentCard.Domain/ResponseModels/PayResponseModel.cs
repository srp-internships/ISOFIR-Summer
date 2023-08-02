namespace StudentCard.Domain.ResponseModels;

public class PayResponseModel
{
    public int Id { get; set; }
    public string Student { get; set; } = "";
    public string Agent { get; set; } = "";
    public DateTime Date { get; set; }
    public decimal Sum { get; set; }
}