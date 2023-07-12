using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using OkResult = Report.Core.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class InvoiceController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IRestService _restService;
    private readonly IStorageService _storageService;
    private readonly IFirmService _firmService;
    private readonly IReportService _reportService;

    public InvoiceController(IProductService productService, ICategoryService categoryService, IRestService restService, IStorageService storageService, IFirmService firmService, IReportService reportService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _restService = restService;
        _storageService = storageService;
        _firmService = firmService;
        _reportService = reportService;
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
                return Redirect(
                    $"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        
        
        var categoryResult = await _categoryService.GetAllAsync();
        switch (categoryResult)
        {
            case OkResult<List<CategoryResponseModel>> categories:
                ViewBag.Categories = categories.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}");
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
        
        var invoicesLogsResult = await _reportService.GetInvoicesLogAsync();
        switch (invoicesLogsResult)
        {
            case OkResult<List<InvoicesLogResponseModel>> invoicesLogs:
                ViewBag.InvoicesLogs = invoicesLogs.Result;
                break;
            case ErrorResult error:
                return Redirect($"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Invoice(InvoiceRequestModel model)
    {
        var response = await _restService.Invoice(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect($"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}