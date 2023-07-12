using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;

namespace Report.Web.Controllers;

public class RestController : Controller
{
    private readonly IRestService _restService;
    private readonly IFirmService _firmService;
    private readonly IStorageService _storageService;
    private readonly IProductService _productService;
    
    public RestController(IRestService restService, IFirmService firmService, IStorageService storageService, IProductService productService)
    {
        _restService = restService;
        _firmService = firmService;
        _storageService = storageService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var productsResult = await _productService.GetAllAsync();
        switch (productsResult)
        {
            case OkResult<List<ProductResponseModel>> products:
                ViewBag.Products = products.Result;
                break;
            case ErrorResult error:
                return Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        
        var firmsResult = await _firmService.GetAllAsync();
        switch (firmsResult)
        {
            case OkResult<List<FirmResponseModel>> firms:
                ViewBag.Firms = firms.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        
        var storageResult = await _storageService.GetAllAsync();
        switch (storageResult)
        {
            case OkResult<List<StorageResponseModel>> storages:
                ViewBag.Storages = storages.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetRestByFilter(RestFilterRequestModel filter)
    {
        var clientsResult = await _restService.GetRestByFilter(filter);

        switch (clientsResult)
        {
            case OkResult<List<RestProductResponseModel>> clients:
                ViewBag.Rests = clients.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        return PartialView();
    }
}