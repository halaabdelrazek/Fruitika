using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using cloudscribe.Pagination.Models;
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
                return View(await PaginatedList<Product>.CreateAsync(_db.Products , pagenumber ,3));
            }


            return View(await PaginatedList<Product>.CreateAsync(_db.Products, pagenumber, 3));
        }


        //public async Task<IActionResult> index(int id, int pagenumber = 1)
        //{
        //    List<Category> categoryList = categoryRepo.GetAll().ToList();
        //    ViewData["categories"] = categoryList;

        //    //if (id != 0)
        //    //{
        //    //    var query = _db.Products.AsNoTracking().OrderBy(p=>p.ProductId);
        //    //    var model = await PagingList.CreateAsync(query, 3, pagenumber);
        //    //    return View(model);
        //    //}

        //    var query = _db.Products.AsNoTracking().OrderBy(p => p.ProductId);
        //    var model = await PagingList.CreateAsync(query, 3, pagenumber);
        //    return View(model);
        //}





        //public IActionResult getProducts(List<Product> prodList)
        //{
        //    prodList = productRepo.get().ToList();
        //    return View(prodList);
        //}

        //public IActionResult paging(int id, int pagenumber = 1, int pagesize = 3)
        //{
        //    List<Category> categoryList = categoryRepo.GetAll().ToList();
        //    ViewData["categories"] = categoryList;
        //    int execludedProducts = (pagenumber * pagesize) - pagesize;

        //    if (id != 0)
        //    {
        //        List<Product> prodbycat = productRepo.getByCategoryId(id).Skip(execludedProducts).Take(pagesize).ToList();
        //        double pagecont = (double)((decimal)productRepo.getByCategoryId(id).Count() / Convert.ToDecimal(pagesize));
        //        ViewData["pagecount"] = (int)Math.Ceiling(pagecont); ;

        //        return Json(prodbycat);
        //    }
        //    List<Product> productList = productRepo.get().Skip(execludedProducts).Take(pagesize).ToList();
        //    return Json(productList);
        //}

        public IActionResult details(int id , ProductDetailsViewModel p2)
        {
            //ProductDetailsViewModel p2 = new ProductDetailsViewModel();
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

        //public IActionResult cart(int id , ProductDetailsViewModel cartDetails)
        //{

        //    Product p = _db.Products.Find(id);
        //    double totalPrice = p.Price * cartDetails.cart.Quantity;
        //    cartDetails.cart.ProductId = id;

        //    return Content($"{cartDetails.cart.Quantity} , {cartDetails.cart.ProductId} {totalPrice}");
        //}

  //      public IActionResult AddCart(int order)
		//{
  //          ViewData["order"] = order;
  //          //productOrderRepo.insert(order);
  //          return View(order);
		//}
    }

   
}
