namespace Report.Web.Controllers;

[Authorize]
public class InvoiceController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IFirmService _firmService;
    private readonly IProductService _productService;
    private readonly IReportService _reportService;
    private readonly IRestService _restService;
    private readonly IStorageService _storageService;
    private readonly string _wrPath;

    public InvoiceController(IProductService productService, ICategoryService categoryService, IRestService restService,
        IStorageService storageService, IFirmService firmService, IReportService reportService,
        IWebHostEnvironment environment)
    {
        _wrPath = environment.WebRootPath;
        _productService = productService;
        _categoryService = categoryService;
        _restService = restService;
        _storageService = storageService;
        _firmService = firmService;
        _reportService = reportService;
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
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }


        var categoryResult = await _categoryService.GetAllAsync(userId);
        switch (categoryResult)
        {
            case OkResult<List<CategoryResponseModel>> categories:
                ViewBag.Categories = categories.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}");
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

        var invoicesLogsResult = await _reportService.GetInvoicesLogAsync(userId);
        switch (invoicesLogsResult)
        {
            case OkResult<List<InvoicesLogResponseModel>> invoicesLogs:
                ViewBag.InvoicesLogs = invoicesLogs.Result;
                break;
            case ErrorResult error:
                return Redirect($"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Invoice(InvoiceRequestModel model)
    {
        var response = await _restService.InvoiceAsync(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }


    [HttpPost]
    public async Task<IActionResult> LoadFromFile(IFormFile? file)
    {
        if (file != null)
        {
            var path = _wrPath + "/" + Guid.NewGuid() + ".xlsx";
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            await file.CopyToAsync(stream);
            var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _restService.InvoiceFromFileAsync(path, true, userId);
            return response switch
            {
                OkResult => RedirectToAction("Index"),
                ErrorResult error => Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
                _ => Redirect($"/ExtraPages/Error?message={500}")
            };
        }

        return RedirectToAction("Index");
    }
}