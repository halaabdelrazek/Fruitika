using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        public CartController(ICartRepository _cartRepository)
        {
            cartRepository = _cartRepository;
        }
        public IActionResult Index()
        {
            string userId = "4587574d-9f4d-40dd-a213-2c69f3a91cd5";
            ProductNameAndImageAndQuantityAndPriceViewModel model = new ProductNameAndImageAndQuantityAndPriceViewModel();

            List<Cart> cartItems = cartRepository.GetCartContent(userId);
            List<Product> cartProducts=cartRepository.GetCartProducts(cartRepository.GetProductsIds(cartItems));

            return View(cartItems);
        }

        [HttpGet("{id:int}")]
        public IActionResult RemoveItem(int id)
        {
            return View(id);

        }
    }
}
