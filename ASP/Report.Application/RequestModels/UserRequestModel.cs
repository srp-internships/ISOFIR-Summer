namespace Report.Application.RequestModels;

public class UserRequestModel
{
    public int Id { get; set; }
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
}