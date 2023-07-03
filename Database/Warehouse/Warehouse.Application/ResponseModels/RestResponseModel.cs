namespace Warehouse.Application.ResponseModels;

public class RestResponseModel
{
    public int Quantity { get; set; }
    public string ProductName { get; set; } = "";
}

public class ClientsHistoryResponseModel
{
    public string ClientName { get; set; } = "";
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    
    public decimal InvoicePrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal Income { get; set; }

    public ClientsHistoryResponseModel BuildIncome()
    {
        Income = SalePrice - InvoicePrice;
        return this;
    }
    
}