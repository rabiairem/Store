using Microsoft.AspNetCore.Mvc;

namespace StoreServiceAPI.Controllers
{
    public class StoreServiceAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
