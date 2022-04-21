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
        private readonly IOrderRepository orderRepository;
        private readonly IProductOrderRepository productOrderRepository;

        public CheckOutController(IProductRepository _productRepository,IOrderRepository _orderRepository,IProductOrderRepository _productOrderRepository)
        {
            productRepository = _productRepository;
            orderRepository = _orderRepository;
            productOrderRepository = _productOrderRepository;
        }

        public IActionResult Index()
        {
            var cartStr = HttpContext.Session.GetString("cartContent").ToString();
            ViewBag.cartSessionStr = cartStr;

            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }


            List<ProductNameAndIdAndQuantityAndPriceViewModel> orderItems = new List<ProductNameAndIdAndQuantityAndPriceViewModel>();

            var cartItemsLC = JsonConvert.DeserializeObject<List<LCViewModel>>(cartStr);
            foreach (var item in cartItemsLC)
            {

                Product product = productRepository.getById(int.Parse(item.Id));
                ProductNameAndIdAndQuantityAndPriceViewModel orderItem = new()
                {
                    ProductId=product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price * item.Quantity
                };
                orderItems.Add(orderItem);

            }

            return View(orderItems);
        }
        [HttpPost]
        public ActionResult PlaceOrder(string name,string email,string phone,string district,string address,string country,string productsList)
        {
          
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order newOrder=new Order() { Address = address, Country = country ,Email=email,ApplicationUserId=userId,Name=name,
                                       Phone=phone,District=district     
                                        };

            orderRepository.Insert(newOrder);
            var orderProductsList = JsonConvert.DeserializeObject<List<ProductNameAndIdAndQuantityAndPriceViewModel>>(productsList);

            foreach(var item in orderProductsList)
            {
                var product=productRepository.getById(item.ProductId);
                productOrderRepository.Insert(product, newOrder, item.Quantity);
            }
            return Content($"Hello {name} {email} {country}{productsList}");
        }
    }
}
