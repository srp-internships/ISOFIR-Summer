namespace Report.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IReportService _reportService;

    public HomeController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        
        var toDayTurnOverResult = await _reportService.GetToDayTurnOverAsync(userId);
        switch (toDayTurnOverResult)
        {
            case OkResult<decimal> toDayTurnOver:
                ViewBag.ToDayTurnOver = toDayTurnOver.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var toDayClearResult = await _reportService.GetToDayClearAsync(userId);

        switch (toDayClearResult)
        {
            case OkResult<decimal> toDayClear:
                ViewBag.ToDayClear = toDayClear.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var toDaySalesResult = await _reportService.GetToDaySalesQuantityAsync(userId);

        switch (toDaySalesResult)
        {
            case OkResult<decimal> toDaySales:
                ViewBag.ToDaySales = toDaySales.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
        return View();
    }
}