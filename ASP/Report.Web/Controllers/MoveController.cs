namespace Report.Web.Controllers;

[Authorize]
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
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var storagesResult = await _storageService.GetAllAsync(userId);

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