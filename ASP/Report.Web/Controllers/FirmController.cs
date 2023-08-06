namespace Report.Web.Controllers;

[Authorize]
public class FirmController : Controller
{
    private readonly IFirmService _firmService;

    public FirmController(IFirmService firmService)
    {
        _firmService = firmService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var firmsResult = await _firmService.GetAllAsync(userId);
        switch (firmsResult)
        {
            case OkResult<List<FirmResponseModel>> firms:
                ViewBag.Firms = firms.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}");
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
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}