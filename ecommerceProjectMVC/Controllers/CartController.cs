using Microsoft.AspNetCore.Mvc;

namespace ecommerceProjectMVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
