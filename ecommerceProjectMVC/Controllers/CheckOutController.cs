using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace ecommerceProjectMVC.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IProductRepository productRepository;

        public CheckOutController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;

        }

        public IActionResult Index()
        {
            var cartStr = HttpContext.Session.GetString("cartContent").ToString();
            ViewBag.cartSessionStr = cartStr;

            //if (User.Identity.IsAuthenticated==false)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string userId = "487574d-9f4d-40dd-a213-2c69f3a91cd5";

            List<ProductNameAndPriceViewModel> orderItems = new List<ProductNameAndPriceViewModel>();

            var cartItemsLC = JsonConvert.DeserializeObject<List<LCViewModel>>(cartStr);
            foreach (var item in cartItemsLC)
            {

                Product product = productRepository.getById(int.Parse(item.Id));
                ProductNameAndPriceViewModel orderItem = new()
                {
                    ProductName = product.ProductName,
                    Price = product.Price * item.Quantity
                };
                orderItems.Add(orderItem);

            }

            return View(orderItems);
        }
    }
}
