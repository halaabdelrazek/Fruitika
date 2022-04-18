using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ecommerceProjectMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;


        IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            ApplicationUser appUser = await userManager.FindByIdAsync(id);

            return View(appUser);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
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

        public async Task<IActionResult> SaveEdit(string id, UserProfileViweModel userView , IFormFile Image)
        {
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

        public async Task<IActionResult> SaveEditPassword(string id, UserProfileViweModel userView)
        {
     
           
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
    }
}
