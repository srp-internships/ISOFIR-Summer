using System.Net;
using Microsoft.AspNetCore.Mvc;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using OkResult = Report.Domain.ActionResults.OkResult;

namespace Report.Web.Controllers;

public class MoveController : Controller
{
    private readonly IRestService _restService;
    private readonly IStorageService _storageService;
    public MoveController(IRestService restService, IStorageService storageService)
    {
        _restService = restService;
        _storageService = storageService;
    }

    public async Task<IActionResult> Index()
    {
        var storagesResult = await _storageService.GetAllAsync();

        switch (storagesResult)
        {
            case OkResult<List<StorageResponseModel>> storages:
                ViewBag.Storages = storages.Result;
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
    public async Task<IActionResult> GetStorageRests(int storageId)
    {
        var restsResult = await _storageService.GetStorageRestsAsync(storageId);

        switch (restsResult)
        {
            case OkResult<List<RestProductResponseModel>> rests:
                ViewBag.Rests = rests.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitMove(List<MoveRequestModel> moveRequestModels)
    {
        var response = await _restService.MoveProductsAsync(moveRequestModels);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}