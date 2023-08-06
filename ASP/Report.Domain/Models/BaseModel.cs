namespace Report.Domain.Models;

public abstract class BaseModel
{
    public int Id { get; set; }

    public BaseModel GetClone()
    {
        return (BaseModel)MemberwiseClone();
    }
}