namespace Report.Web.Controllers;

[Authorize]
public class SaleController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IClientService _clientService;
    private readonly IRateService _rateService;
    private readonly IReportService _reportService;
    private readonly IRestService _restService;

    public SaleController(IRestService restService, ICategoryService categoryService, IClientService clientService,
        IReportService reportService, IRateService rateService)
    {
        _restService = restService;
        _categoryService = categoryService;
        _clientService = clientService;
        _reportService = reportService;
        _rateService = rateService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var rateResult = await _rateService.GetLastAsync(userId);

        switch (rateResult)
        {
            case OkResult<decimal> rate:
                ViewBag.Rate = rate.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var categoriesResult = await _categoryService.GetAllAsync(userId);

        switch (categoriesResult)
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

        var clientsResult = await _clientService.GetClientsForSelectAsync(userId);

        switch (clientsResult)
        {
            case OkResult<List<GetClientForSelectResponseModel>> clients:
                ViewBag.Clients = clients.Result;
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
    public async Task<IActionResult> Sale(SaleRequestModel saleRequestModel)
    {
        var response = await _restService.SaleAsync(saleRequestModel);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }

    [HttpPost]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var productResult = await _restService.GetListOfRestProductsByCategoryAsync(categoryId);

        switch (productResult)
        {
            case OkResult<List<GetRestProductsNameResponseModel>> products:
                ViewBag.RestProducts = products.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> GetRestsByProduct(int productId)
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var productResult = await _restService.GetRestsByProductAsync(productId, userId);

        switch (productResult)
        {
            case OkResult<List<GetRestsByProductResponseModel>> products:
                ViewBag.RestProducts = products.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> GetClientHistory(int clientId)
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var clientHistoryResult = await _reportService.GetClientHistoryAsync(clientId, userId);

        switch (clientHistoryResult)
        {
            case OkResult<List<SaleLogResponseModel>> clientHistory:
                ViewBag.ClientHistory = clientHistory.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return PartialView();
    }

    public async Task<IActionResult> GetClientInfo(int clientId)
    {
        var clientResult = await _clientService.GetByIdAsync(clientId);
        string result;
        switch (clientResult)
        {
            case OkResult<ClientResponseModel> client:
                result =
                    $"<label>Информация о клиенте</label><br/><label>Имя: {client.Result.Name}</label><br/><label>Телефон: {client.Result.Phone}</label><br/><label>Доход: {client.Result.Income}</label><br/><label>Оборот: {client.Result.Sales}</label><br/><label>Счёт: {client.Result.CashTjs}</label><br/><label></label><br/><label>Оплаты клиента</label><br/>";
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var paysResult = await _clientService.GetClientPaysAsync(clientId);

        switch (paysResult)
        {
            case OkResult<List<ClientCashLogResponseModel>> logs:
                result += @"
                    <table class=""table"">
                        <thead><tr><th>Дата</th><th>Касса</th><th>TJS</th><th>USD</th></tr></thead>
                        <tbody>          
                    ";
                result = logs.Result.Aggregate(result,
                    (current, log) =>
                        current +
                        @$"<td>{log.DateTime}</td><td>{log.CashBox}</td><td>{log.CashTjs}</td><td>{log.CashUsd}</td>");
                result += "</tbody></table>";
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return Content(result, "text/html");
    }
}