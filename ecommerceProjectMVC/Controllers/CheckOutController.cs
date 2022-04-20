using Microsoft.AspNetCore.Mvc;

namespace ecommerceProjectMVC.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
