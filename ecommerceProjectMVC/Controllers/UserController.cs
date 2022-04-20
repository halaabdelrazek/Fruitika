using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ecommerceProjectMVC.Repositories;

namespace ecommerceProjectMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ContextEntities context;
		private readonly IProductOrderRepository prorRepo;
		private readonly IOrderRepository orderRepo;
		IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ContextEntities context,IProductOrderRepository prorRepo,IOrderRepository orderRepo)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
			this.prorRepo = prorRepo;
			this.orderRepo = orderRepo;
		}


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser appUser = await userManager.FindByIdAsync(id);

            return View(appUser);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser appUser = await userManager.FindByIdAsync(id);

            UserProfileViweModel userViewModel = new UserProfileViweModel();

            userViewModel.Id = appUser.Id;
            userViewModel.UserName = appUser.UserName;

            userViewModel.PhoneNumber = appUser.PhoneNumber;

            userViewModel.Address = appUser.Address;
            userViewModel.Image = appUser.Image;
            userViewModel.Email = appUser.Email;

            return View("Edit", userViewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SaveEdit( UserProfileViweModel userView , IFormFile Image)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser appUser = await userManager.FindByIdAsync(id);

            if (ModelState.IsValid == false)
            {
                return View("Edit", userView);
            }
           try
            {
                
                appUser.Address = userView.Address;
                appUser.Email = userView.Email;
                appUser.UserName = userView.UserName;
                appUser.PhoneNumber = userView.PhoneNumber;

                if(Image == null)
                {

                    IdentityResult _result = await userManager.UpdateAsync(appUser);
                    if (_result.Succeeded == true)
                    {
                        return RedirectToAction("Details", "user", new { id = appUser.Id });
                    }
                    return RedirectToAction("Edit", "user", new { id = appUser.Id });
                }

                
                if(Image.FileName is not null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets", "img");

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                  
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                        fileStream.Close();
                    }
                    appUser.Image = uniqueFileName;

                }
              
                
                IdentityResult result = await userManager.UpdateAsync(appUser);
                if (result.Succeeded == true)
                {
                    return RedirectToAction("Details", "user", new {id = appUser.Id });
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("Details", "user", new { id = appUser.Id });



        }

        public async Task<IActionResult> SaveEditPassword( UserProfileViweModel userView)
        {
     
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser appUser = await userManager.FindByIdAsync(id);
            
            userView.Email = appUser.Email;
            userView.Address = appUser.Address;
            userView.PhoneNumber = appUser.PhoneNumber;
            userView.UserName = appUser.UserName;
            userView.Image = appUser.Image;
            if (userView.OldPassword == null|| userView.NewPassword == null)
            {
                
                 return View("Edit", userView);
               

            }
            try
            {
                IdentityResult result = await userManager.ChangePasswordAsync(appUser, userView.OldPassword, userView.NewPassword);
                if (result.Succeeded == true)
                {
                    return RedirectToAction("Details", "user", new { id = appUser.Id });

                }
                else
                {
                    ModelState.AddModelError("", "old password is not correct");
                    return RedirectToAction("Edit", "user", new { id = appUser.Id });

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("Details", "user", new { id = appUser.Id });





        }
        [Authorize]
        public async Task<ActionResult> GetUserOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<int> userorderidsList = orderRepo.GetOrderIdsByUser(userId);
            List<Order> userorderList = orderRepo.GetOrdersByUser(userId);
            ViewBag.orders = userorderList;
            List<ProductOrder> userProductOrderList = prorRepo.GetProductOrdersByOrderIds(userorderidsList);
            return View(userProductOrderList);
        }
    }
}
