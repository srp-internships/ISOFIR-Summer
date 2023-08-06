namespace Report.Web.Controllers;

[Authorize]
public class RestController : Controller
{
    private readonly IFirmService _firmService;
    private readonly IProductService _productService;
    private readonly IRestService _restService;
    private readonly IStorageService _storageService;

    public RestController(IRestService restService, IFirmService firmService, IStorageService storageService,
        IProductService productService)
    {
        _restService = restService;
        _firmService = firmService;
        _storageService = storageService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var productsResult = await _productService.GetAllAsync(userId);
        switch (productsResult)
        {
            case OkResult<List<ProductResponseModel>> products:
                ViewBag.Products = products.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var firmsResult = await _firmService.GetAllAsync(userId);
        switch (firmsResult)
        {
            case OkResult<List<FirmResponseModel>> firms:
                ViewBag.Firms = firms.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var storageResult = await _storageService.GetAllAsync(userId);
        switch (storageResult)
        {
            case OkResult<List<StorageResponseModel>> storages:
                ViewBag.Storages = storages.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetRestByFilter(RestFilterRequestModel filter)
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        filter.UserId = userId;
        var clientsResult = await _restService.GetRestByFilterAsync(filter);

        switch (clientsResult)
        {
            case OkResult<List<RestProductResponseModel>> clients:
                ViewBag.Rests = clients.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }
}