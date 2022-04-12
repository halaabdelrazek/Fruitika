using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Controllers
{
    public class HomePageController : Controller
    {
        ICategoryRepository CategoryRepo;

        public HomePageController(ICategoryRepository _CategoryRepo)
        {
            CategoryRepo = _CategoryRepo;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = CategoryRepo.GetAll();
            return View(categoryList);
        }
    }
}
