using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using OkResult = Report.Core.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class StorageController : Controller
{
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<IActionResult> Index()
    {
        var storageResult = await _storageService.GetAllAsync();
        
        switch (storageResult)
        {
            case OkResult<List<StorageResponseModel>> storages:
                ViewBag.Storages = storages.Result;
                break;
            case ErrorResult error:
                return Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddStorage(StorageRequestModel storage)
    {
        var response = await _storageService.CreateOrUpdate(storage);

        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}