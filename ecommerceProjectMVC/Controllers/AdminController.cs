using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Controllers
{
    public class AdminController : Controller
    {
        IProductRepository productRepository;
        ICategoryRepository categoryRepository;

        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        
        public IActionResult Index()
        {
            List<Product> products = productRepository.GetAllIncludeCategory();
            List<Category> categories = categoryRepository.GetAll();
            ViewData["Categories"] = categories;
            return View(products);
        }

        [AcceptVerbs]
        public IActionResult Add(Product newProduct)
        {
            int affectedRows = productRepository.add(newProduct);
            //List<Product> products = productRepository.GetAllIncludeCategory();
            return Json(true);
        }


        public IActionResult GetAll()
        {
            List<Product> products = productRepository.GetAllIncludeCategory();
            return Json(products);
        }

        //public IActionResult GetById(int id)
        //{

        //}

        public IActionResult Remove(int productId)
        {
            productRepository.delete(productId);
            return Json(true);
        }

        public IActionResult Update(int id, Product product)
        {
            productRepository.update(id, product);
            return Json(true);
        }

    }
}
