namespace Report.Application.RequestModels;

public class SaleRequestModel : BaseRequestModel
{
    public decimal _priceTjs;
    public decimal _priceUsd;
    public int RestProductId { get; set; }
    public int Quantity { get; set; }

    public string PriceTjs
    {
        get => _priceTjs.ToString();
        set => _priceTjs = decimal.Parse(value.Replace('.', ','));
    }

    public string PriceUsd
    {
        get => _priceUsd.ToString();
        set => _priceUsd = decimal.Parse(value.Replace('.', ','));
    }

    public DateTime DateTime { get; set; } = DateTime.Now;
    public int ClientId { get; set; }
}