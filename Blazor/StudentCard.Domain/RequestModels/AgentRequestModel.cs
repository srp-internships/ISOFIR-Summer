namespace StudentCard.Domain.RequestModels;

public class AgentRequestModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}