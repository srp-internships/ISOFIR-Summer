namespace Report.Web.Controllers;

[Authorize]
public class ClientCashBoxController : Controller
{
    private readonly ICashBoxService _cashBoxService;
    private readonly IClientCashBoxService _clientCashBoxService;
    private readonly IClientService _clientService;

    public ClientCashBoxController(ICashBoxService cashBoxService, IClientCashBoxService clientCashBoxService,
        IClientService clientService)
    {
        _cashBoxService = cashBoxService;
        _clientCashBoxService = clientCashBoxService;
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var cashBoxesResult = await _cashBoxService.GetAllAsync(userId);

        switch (cashBoxesResult)
        {
            case OkResult<List<CashBoxResponseModel>> cashBoxes:
                ViewBag.CashBoxes = cashBoxes.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var clientsHistoryResult = await _clientCashBoxService.GetAllHistoryAsync(userId);

        switch (clientsHistoryResult)
        {
            case OkResult<List<ClientCashBoxResponseModel>> clientsHistories:
                ViewBag.History = clientsHistories.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + Environment.NewLine + error.Exception)}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        var clientsResult = await _clientService.GetAllAsync(userId);

        switch (clientsResult)
        {
            case OkResult<List<ClientResponseModel>> clients:
                ViewBag.Clients = clients.Result;
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
    public async Task<IActionResult> Submit(ClientCashBoxRequestModel model)
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        model.UserId = userId;
        var response = await _clientCashBoxService.PayWithDraw(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }

    [HttpPost]
    public async Task<IActionResult> Pay(ClientCashBoxRequestModel model)
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        model.UserId = userId;
        var response = await _clientCashBoxService.PayWithDraw(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }

    [HttpPost]
    public async Task<IActionResult> WithDraw(ClientCashBoxRequestModel model)
    {
        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        model.UserId = userId;

        model.CashUsd = -model.CashUsd;
        model.CashTjs = -model.CashTjs;
        var response = await _clientCashBoxService.PayWithDraw(model);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}