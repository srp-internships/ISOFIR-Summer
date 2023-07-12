using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using OkResult = Report.Core.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var clientsResult = await _clientService.GetAllAsync();
        
        switch (clientsResult)
        {
            case OkResult<List<ClientResponseModel>> clients:
                ViewBag.Clients = clients.Result;
                break;
            case ErrorResult error:
                return Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    public async Task<IActionResult> AddClient(ClientRequestModel client)
    {
            var response = await _clientService.CreateOrUpdate(client);
            return response switch
            {
                OkResult => RedirectToAction("Index"),
                ErrorResult error => Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}"),
                _ => Redirect($"/ExtraPages/Error?message={500}")
            };
        }
}