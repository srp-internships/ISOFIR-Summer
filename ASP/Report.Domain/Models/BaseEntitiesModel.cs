namespace Report.Domain.Models;

public abstract class BaseEntitiesModel : BaseModel
{
    public int UserId { get; set; }
    public User? User { get; set; }
}