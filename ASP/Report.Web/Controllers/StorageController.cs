namespace Report.Web.Controllers;

[Authorize]
public class StorageController : Controller
{
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var storageResult = await _storageService.GetAllAsync(userId);

        switch (storageResult)
        {
            case OkResult<List<StorageResponseModel>> storages:
                ViewBag.Storages = storages.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddStorage(StorageRequestModel storage)
    {
        var response = await _storageService.CreateOrUpdateAsync(storage);

        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}