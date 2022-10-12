using Microsoft.AspNetCore.Mvc;

namespace Schedule.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}