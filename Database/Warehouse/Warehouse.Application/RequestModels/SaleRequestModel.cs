﻿namespace Warehouse.Application.RequestModels;

public class SaleRequestModel
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public int ClientId { get; set; }
}