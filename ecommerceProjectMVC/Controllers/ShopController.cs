using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using ecommerceProjectMVC.viewModel;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ecommerceProjectMVC.Controllers
{
    public class ShopController : Controller
    {
        ContextEntities _db ;

        IProductRepository productRepo;
        ICategoryRepository categoryRepo;
        IProductOrderRepository productOrderRepo;
        public ShopController(IProductRepository _productRepo, ICategoryRepository _categoryRepo, ContextEntities db)
        {
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
            _db = db;
        }
        public async Task<IActionResult> index(int id, int pagenumber = 1)
        {
            List<Category> categoryList = categoryRepo.GetAll().ToList();
            ViewData["categories"] = categoryList;

            if (id != 0)
            {
                var prodByCat = _db.Products.Where(p=> p.CategoryId == id);
                return View(await PaginatedList<Product>.CreateAsync(prodByCat, pagenumber ,3));
            }


            return View(await PaginatedList<Product>.CreateAsync(_db.Products, pagenumber, 3));
        }





        public IActionResult details(int id , ProductDetailsViewModel p2)
        {
            Product p = productRepo.getById(id);

            if(p == null)
            {
                return View(new Product());
            }
            if (p2.cart == null)
            {
                p2.cart = new Cart();
            }
            p2.cart.ProductId= id;
            p2.cart.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            p2.product = p;
            
            return View(p2);
        }

    }

   
}
