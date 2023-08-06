﻿using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IProductService
{
    public Task<Result> CreateOrUpdateAsync(ProductRequestModel productDto);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync(int userId);

    public Task<Result> LoadFromExcelAsync(string path, bool mode);
}