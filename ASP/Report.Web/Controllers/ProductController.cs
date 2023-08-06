namespace Report.Web.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly string _wrPath;

    public ProductController(IProductService productService, ICategoryService categoryService,
        IWebHostEnvironment environment)
    {
        _wrPath = environment.WebRootPath;
        _productService = productService;
        _categoryService = categoryService;
    }

    [Route("/Dashboard/Products")]
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
    public async Task<IActionResult> AddProduct(ProductRequestModel product)
    {
        var response = await _productService.CreateOrUpdateAsync(product);
        return response switch
        {
            OkResult => RedirectToAction("Index"),
            ErrorResult error => Redirect(
                $"/ExtraPages/Error?message={error.Message + WebUtility.UrlEncode(error.Exception.ToString())}"),
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
            var response = await _productService.LoadFromExcelAsync(path, true);
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