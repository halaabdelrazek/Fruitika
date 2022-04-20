using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Controllers
{

    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;  
        public CartController(ICartRepository _cartRepository,IProductRepository _productRepository)
        {
            cartRepository = _cartRepository;
            productRepository = _productRepository;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            string userId = "4587574d-9f4d-40dd-a213-2c69f3a91cd5";
            ViewBag.cartContent = JsonConvert.DeserializeObject<List<LCViewModel>>((string)TempData["cartContentJson"]);
            List<ProductNameAndImageAndQuantityAndPriceViewModel> cartItems = new List<ProductNameAndImageAndQuantityAndPriceViewModel>();

            foreach (var item in ViewBag.cartContent)
            {
                Product product = productRepository.getById(int.Parse(item.Id));
                cartItems.Add(new ProductNameAndImageAndQuantityAndPriceViewModel(){ProductId=product.ProductId,
                                                                                    ProductName=product.ProductName,
                                                                                    ProductImage=product.Image,
                                                                                    UnitPrice=product.Price,
                                                                                    TotalPrice=product.Price*item.Quantity,
                                                                                    Quantity=item.Quantity
                                                                                     });
            }
            //List<Cart> cartItems = cartRepository.GetCartContent(userId);
            //List<Product> cartProducts=cartRepository.GetCartProducts(cartRepository.GetProductsIds(cartItems));
            return View(cartItems);
        }

        [HttpPost]
        public ActionResult GetData(string lcInput)
        {
            List<LCViewModel> cartList = JsonConvert.DeserializeObject<List<LCViewModel>>(lcInput);
            TempData["cartContentJson"] = JsonConvert.SerializeObject(cartList);
            return RedirectToAction("Index");
        }

        [HttpGet("{id:int}")]
        public IActionResult RemoveItem(int id)
        {
            return View(id);

        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
    }
}
