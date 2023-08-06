namespace Report.Web.Controllers;

[Authorize]
public class ReasonCashBoxController : Controller
{
    private readonly ICashBoxService _cashBoxService;
    private readonly IReasonCashBoxService _reasonCashBoxService;
    private readonly IReasonService _reasonService;

    public ReasonCashBoxController(ICashBoxService cashBoxService, IReasonCashBoxService reasonCashBoxService,
        IReasonService reasonService)
    {
        _cashBoxService = cashBoxService;
        _reasonCashBoxService = reasonCashBoxService;
        _reasonService = reasonService;
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

        var reasonHistoryResult = await _reasonCashBoxService.GetAllHistoryAsync(userId);

        switch (reasonHistoryResult)
        {
            case OkResult<List<ReasonCashBoxResponseModel>> clientsHistories:
                ViewBag.History = clientsHistories.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var reasonsResult = await _reasonService.GetAllAsync(userId);

        switch (reasonsResult)
        {
            case OkResult<List<ReasonResponseModel>> reasons:
                ViewBag.Reasons = reasons.Result;
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
    public async Task<IActionResult> Submit(ReasonCashBoxRequestModel model)
    {
        var response = await _reasonCashBoxService.PayWithDraw(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }

    [HttpPost]
    public async Task<IActionResult> Pay(ReasonCashBoxRequestModel model)
    {
        var response = await _reasonCashBoxService.PayWithDraw(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }

    [HttpPost]
    public async Task<IActionResult> WithDraw(ReasonCashBoxRequestModel model)
    {
        model.CashUsd = -model.CashUsd;
        model.CashTjs = -model.CashTjs;
        var response = await _reasonCashBoxService.PayWithDraw(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}