﻿namespace Report.Core.Models;

public class Category:BaseModel
{
    public string Name { get; set; } = string.Empty;
    public List<Product>? Products { get; set; }
}