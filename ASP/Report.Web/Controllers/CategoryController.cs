using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using OkResult = Report.Core.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Route("/Dashboard/Categories")]
    public async Task<IActionResult> Categories()
    {
        var categoriesResult = await _categoryService.GetAllAsync();
        
        switch (categoriesResult)
        {
            case OkResult<List<CategoryResponseModel>> categories:
                ViewBag.Categories = categories.Result;
                break;
            case ErrorResult error:
                return Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
            
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryRequestModel category)
    {
        var response = await _categoryService.CreateOrUpdate(category);
        return response switch
        {
            OkResult => RedirectToAction("Categories"),
            ErrorResult error => Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}