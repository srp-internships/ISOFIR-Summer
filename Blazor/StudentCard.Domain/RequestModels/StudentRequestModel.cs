namespace StudentCard.Domain.RequestModels;

public class StudentRequestModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string SpecialtyCode { get; set; }="";
    public string Department { get; set; }="";
    public int Course { get; set; }
    public string Group { get; set; } = "";
    public decimal Dept { get; set; }
}