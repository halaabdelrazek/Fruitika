using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Controllers
{
    public class ShopController : Controller
    {
        ContextEntities db ;

        IProductRepository productRepo;
        public ShopController(IProductRepository _productRepo)
        {
            productRepo = _productRepo;
        }
        public IActionResult index(int id)
        {
            if (id !=0)
            {
                List<Product> prodByCat = productRepo.getByCategoryId(id);
                return View(prodByCat);
            }

            List<Product> productList = productRepo.get();

            return View(productList);
        }

        public IActionResult details(int id)
        {
            
            Product p = productRepo.getById(id);
            if(p == null)
            {
                return View(new Product());
            }
            return View(p);
        }
    }
}
