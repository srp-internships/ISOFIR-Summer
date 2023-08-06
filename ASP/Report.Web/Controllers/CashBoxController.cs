﻿namespace Report.Web.Controllers;

[Authorize]
public class CashBoxController : Controller
{
    private readonly ICashBoxService _cashBoxService;

    public CashBoxController(ICashBoxService cashBoxService)
    {
        _cashBoxService = cashBoxService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        var cashBoxesResult = await _cashBoxService.GetAllAsync(userId);

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