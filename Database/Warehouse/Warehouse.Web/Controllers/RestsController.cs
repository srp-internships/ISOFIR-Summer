using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Common.Interfaces.Services;

namespace Warehouse.Web.Controllers;

public class RestsController : Controller
{
    private readonly IRestService _restService;

    public RestsController(IRestService restService)
    {
        _restService = restService;
    }

    public IActionResult Index()
    {
        ViewBag.Rests = _restService.GetRests();
        return View();
    }
}