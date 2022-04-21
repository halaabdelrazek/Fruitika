using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Http;
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
            List<ProductNameAndImageAndQuantityAndPriceViewModel> cartItems = new List<ProductNameAndImageAndQuantityAndPriceViewModel>();
            #region oldCode
            //if (TempData["cartContentJson"] is not null)
            //{
            //    ViewBag.cartContent = JsonConvert.DeserializeObject<List<LCViewModel>>((string)TempData["cartContentJson"]);

            //    foreach (var item in ViewBag.cartContent)
            //    {

            //        Product product = productRepository.getById(int.Parse(item.Id));
            //        ProductNameAndImageAndQuantityAndPriceViewModel  cartItem= new ()
            //        {
            //            ProductId = product.ProductId,
            //            ProductName = product.ProductName,
            //            ProductImage = product.Image,
            //            UnitPrice = product.Price,
            //            TotalPrice = product.Price * item.Quantity,
            //            Quantity = item.Quantity
            //        };
            //        cartItems.Add(cartItem);
            //    }

            //    //string cartSessionStr = JsonConvert.SerializeObject(cartItems);
            //}
            //else
            //{
            #endregion
            var cartStr = HttpContext.Session.GetString("cartContent").ToString();
            //ViewBag.cartSessionStr=cartStr;
                if(cartStr is not null)
                 {
                     var cartItemsLC = JsonConvert.DeserializeObject<List<LCViewModel>>(cartStr);
                    foreach (var item in cartItemsLC)
                    {

                        Product product = productRepository.getById(int.Parse(item.Id));
                        ProductNameAndImageAndQuantityAndPriceViewModel cartItem = new()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductImage = product.Image,
                            UnitPrice = product.Price,
                            TotalPrice = product.Price * item.Quantity,
                            Quantity = item.Quantity
                        };
                        cartItems.Add(cartItem);

                     }
                }
            return View(cartItems);
        }

        [HttpPost]
        public ActionResult GetData(string lcInput)
        {
            #region oldcode
            //    List<LCViewModel> cartList = JsonConvert.DeserializeObject<List<LCViewModel>>(lcInput);
            //    TempData["cartContentJson"] = JsonConvert.SerializeObject(cartList);
            //HttpContext.Session.SetString("cartContent", (string)TempData["cartContentJson"]);
            #endregion
            HttpContext.Session.SetString("cartContent",lcInput);
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
