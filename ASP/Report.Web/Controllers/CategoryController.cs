namespace Report.Web.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Route("/Dashboard/Categories")]
    public async Task<IActionResult> Categories()
    {
        var userId = int.Parse(HttpContext.User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);

        var categoriesResult = await _categoryService.GetAllAsync(userId);

        switch (categoriesResult)
        {
            case OkResult<List<CategoryResponseModel>> categories:
                ViewBag.Categories = categories.Result;
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
    public async Task<IActionResult> AddCategory(CategoryRequestModel category)
    {
        var response = await _categoryService.CreateOrUpdateAsync(category);
        return response switch
        {
            OkResult => RedirectToAction("Categories"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={WebUtility.UrlEncode(error.Message + error.Exception)}"),
            _ => Redirect($"/ExtraPages/Error?message={500}")
        };
    }
}