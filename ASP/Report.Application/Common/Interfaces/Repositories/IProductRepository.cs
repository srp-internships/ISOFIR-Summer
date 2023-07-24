﻿using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IProductRepository:IRepository<Product>
{
    public Task<int?> GetIdByNameAsync(string name);

}
