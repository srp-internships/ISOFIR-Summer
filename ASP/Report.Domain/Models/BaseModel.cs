namespace Report.Core.Models;

public class BaseModel
{
    public int Id { get; set; }

    public BaseModel GetClone()
    {
        return (BaseModel)MemberwiseClone();
    }
}