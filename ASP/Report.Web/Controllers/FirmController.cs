using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using OkResult = Report.Domain.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class FirmController : Controller
{
    private readonly IFirmService _firmService;

    public FirmController(IFirmService firmService)
    {
        _firmService = firmService;
    }

    public async Task<IActionResult> Index()
    {
        var firmsResult = await _firmService.GetAllAsync();
        switch (firmsResult)
        {
            case OkResult<List<FirmResponseModel>> firms:
                ViewBag.Firms = firms.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    public async Task<IActionResult> AddFirm(FirmRequestModel model)
    {
        var response = await _firmService.CreateOrUpdateAsync(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect($"/ExtraPages/Error?message={error.Message + System.Net.WebUtility.UrlEncode(error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}