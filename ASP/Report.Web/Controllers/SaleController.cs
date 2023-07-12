using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using OkResult = Report.Core.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class SaleController : Controller
{
    private readonly IRestService _restService;

    private readonly IClientService _clientService;
    private readonly ICategoryService _categoryService;

    public SaleController(IRestService restService, ICategoryService categoryService, IClientService clientService)
    {
        _restService = restService;
        _categoryService = categoryService;
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var categoriesResult = await _categoryService.GetAllAsync();
        
        switch (categoriesResult)
        {
            case OkResult<List<CategoryResponseModel>> categories:
                ViewBag.Categories = categories.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        var clientsResult = await _clientService.GetClientsForSelectAsync();
        
        switch (clientsResult)
        {
            case OkResult<List<GetClientForSelectResponseModel>> clients:
                ViewBag.Clients = clients.Result;
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
    public async Task<IActionResult> Sale(SaleRequestModel saleRequestModel)
    {
        var response = await _restService.Sale(saleRequestModel);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect($"/ExtraPages/Error?message={System.Net.WebUtility.UrlEncode(error.Message + error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };

    }

    [HttpPost]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        
        var productResult = await _restService.GetListOfRestProductsByCategory(categoryId);

        switch (productResult)
        {
            case OkResult<List<GetRestProductsNameResponseModel>> products:
                ViewBag.RestProducts = products.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> GetRestsByProduct(int productId)
    {
        var productResult = await _restService.GetRestsByProduct(productId);

        switch (productResult)
        {
            case OkResult<List<GetRestsByProductResponseModel>> products:
                ViewBag.RestProducts = products.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }
}