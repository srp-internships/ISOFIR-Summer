using System.Net;
using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using OkResult = Report.Domain.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class CashBoxController : Controller
{
    private readonly ICashBoxService _cashBoxService;

    public CashBoxController(ICashBoxService cashBoxService)
    {
        _cashBoxService = cashBoxService;
    }

    public async Task<IActionResult> Index()
    {
        var cashBoxesResult = await _cashBoxService.GetAllAsync();

        switch (cashBoxesResult)
        {
            case OkResult<List<CashBoxResponseModel>> cashBoxes:
                ViewBag.CashBoxes = cashBoxes.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCashBox(CashBoxRequestModel model)
    {
        var response = await _cashBoxService.CreateOrUpdateAsync(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}