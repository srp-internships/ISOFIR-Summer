using StudentCard.Domain.Models;

namespace StudentCard.Domain.ResponseModels;

public class AgentResponseModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Role { get; set; } = "";
    public List<PayResponseModel>? Pay { get; set; }
}