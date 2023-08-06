namespace Report.Web.Controllers;

[Authorize]
public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var clientsResult = await _clientService.GetAllAsync(userId);

        switch (clientsResult)
        {
            case OkResult<List<ClientResponseModel>> clients:
                ViewBag.Clients = clients.Result;
                break;
            case ErrorResult error:
                return Redirect(
                    $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}");
            default:
                return Redirect($"/ExtraPages/Error?message={500}");
        }

        return View();
    }

    public async Task<IActionResult> AddClient(ClientRequestModel client)
    {
        var response = await _clientService.CreateOrUpdateAsync(client);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}