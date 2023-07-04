using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;

namespace Warehouse.Web.Controllers;

public class InvoiceController : Controller
{
    private readonly IProductService _productService;
    private readonly IRestService _restService;
    private readonly IReportService _reportService;

    public InvoiceController(IReportService reportService, IProductService productService, IRestService restService)
    {
        _reportService = reportService;
        _productService = productService;
        _restService = restService;
    }

    public IActionResult Index()
    {
        ViewBag.Products = _productService.GetAll();
        ViewBag.Invoices = _reportService.GetInvoicesHistory();
        return View();
    }

    public IActionResult Invoice(InvoiceRequestModel model)
    {
        _restService.Invoice(model);
        return RedirectToAction("Index", "Invoice");
    }
}