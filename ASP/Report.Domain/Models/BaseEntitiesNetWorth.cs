namespace Report.Domain.Models;

public abstract class BaseEntitiesNetWorth : BaseEntitiesModel
{
    public decimal CashTjs { get; set; }
    public decimal CashUsd { get; set; }
}