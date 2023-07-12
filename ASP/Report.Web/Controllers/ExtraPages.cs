using Microsoft.AspNetCore.Mvc;

namespace Report.Web.Controllers;

public class ExtraPages : Controller
{
    // GET
    public IActionResult Error(string message)
    {
        ViewBag.ErrorMessage = message;
        return View();
    }
}