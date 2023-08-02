namespace StudentCard.Domain.Models;

public class Pay
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int AgentId { get; set; }
    public Agent? Agent { get; set; }
    public decimal Sum { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
}