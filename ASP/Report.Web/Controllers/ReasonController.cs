namespace Report.Web.Controllers;

[Authorize]
public class ReasonController : Controller
{
    private readonly IReasonService _reasonService;

    public ReasonController(IReasonService reasonService)
    {
        _reasonService = reasonService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

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
    public async Task<IActionResult> CreateOrUpdateReason(ReasonRequestModel model)
    {
        var response = await _reasonService.CreateOrUpdateAsync(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}