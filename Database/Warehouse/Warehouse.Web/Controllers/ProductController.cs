using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;

namespace Warehouse.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewBag.Products = _productService.GetAll();
        return View();
    }

    public IActionResult AddProduct(ProductRequestModel model)
    {
        _productService.CreateOrEdit(model);
        return RedirectToAction("Index", "Product");
    }
}