using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;

namespace Warehouse.Web.Controllers;

public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public IActionResult Index()
    {
        ViewBag.Clients = _clientService.GetAll().ToList(); 
        return View();
    }

    public IActionResult AddClient(ClientRequestModel requestModel)
    {
        _clientService.CreateOrEdit(requestModel);
        return RedirectToAction("Index", "Client");
    }
}