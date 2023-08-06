namespace Report.Web.Controllers;

[Authorize]
public class ReportController : Controller
{
    private readonly IReportService _reportService;
    private readonly IStorageService _storageService;
    private readonly string _wrPath;
    public ReportController(IStorageService storageService, IReportService reportService,IWebHostEnvironment webHostEnvironment)
    {
        _storageService = storageService;
        _reportService = reportService;
        _wrPath = webHostEnvironment.WebRootPath;
    }

    public async Task<IActionResult> RestInStorage()
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
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

    public async Task<IActionResult> GetStorageRest(int storageId)
    {
        // var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var storageRestsResult = await _reportService.GetStorageRestAsync(storageId);

        switch (storageRestsResult)
        {
            case OkResult<List<RestProductResponseModel>> storageRests:
                ViewBag.Rests = storageRests.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }

    public async Task<IActionResult> ExportStorageRestsToExcel()
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        var fileName=$"Rest{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
        
        var storageRestsResult = await _reportService.ExportRestsToExcelAsync(userId,Path.GetFullPath(_wrPath+"/"+fileName));

        switch (storageRestsResult)
        {
            case OkResult:
                return File(fileName,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
    }

    public async Task<IActionResult> ExportDebtsToExcel()
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        
        var fileName=$"Rest{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
        
        var storageRestsResult = await _reportService.ExportDebtsToExcelAsync(userId,Path.GetFullPath(_wrPath+"/"+fileName));

        switch (storageRestsResult)
        {
            case OkResult:
                return File(fileName,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }
    }
}