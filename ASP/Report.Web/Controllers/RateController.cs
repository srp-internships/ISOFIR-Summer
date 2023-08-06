namespace Report.Web.Controllers;

[Authorize]
public class RateController : Controller
{
    private readonly IRateService _rateService;

    public RateController(IRateService rateService)
    {
        _rateService = rateService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var ratesResult = await _rateService.GetAllAsync(userId);

        switch (ratesResult)
        {
            case OkResult<List<RateResponseModel>> rates:
                ViewBag.Rates = rates.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    public async Task<IActionResult> AddRate(decimal oneDollarIs)
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var response = await _rateService.CreateAsync(oneDollarIs, userId);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}