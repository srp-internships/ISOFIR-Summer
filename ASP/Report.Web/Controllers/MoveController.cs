using Microsoft.AspNetCore.Mvc;

namespace Report.Web.Controllers;

public class MoveController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}