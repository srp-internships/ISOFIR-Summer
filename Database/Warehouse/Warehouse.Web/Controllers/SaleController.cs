using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;

namespace Warehouse.Web.Controllers;

public class SaleController : Controller
{
    private readonly IClientService _clientService;
    private readonly IProductService _productService;
    private readonly IRestService _restService;
    private readonly IReportService _reportService;

    public SaleController(IClientService clientService, IProductService productService, IRestService restService, IReportService reportService)
    {
        _clientService = clientService;
        _productService = productService;
        _restService = restService;
        _reportService = reportService;
    }

    public IActionResult Index()
    {
        ViewBag.Clients = _clientService.GetAll();
        ViewBag.Products = _productService.GetAll();
        ViewBag.ClientsHistory = _reportService.GetClientsHistory();
        
        return View();
    }

    public IActionResult Sale(SaleRequestModel requestModel)
    {
        _restService.Sale(requestModel);
        return RedirectToAction("Index", "Sale");
    }
}