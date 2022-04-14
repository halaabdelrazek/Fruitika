﻿using ecommerceProjectMVC.Models;
using ecommerceProjectMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ecommerceProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {

            if (ModelState.IsValid == false)
            {
                return View(newUser);
            }
            try
            {
                ApplicationUser newUserModel = new ApplicationUser();
                newUserModel.UserName = newUser.UserName;
                newUserModel.Address = newUser.Address;
                newUserModel.PasswordHash = newUser.Password;
                newUserModel.Image = newUser.Image;

                IdentityResult result = await userManager.CreateAsync(newUserModel, newUser.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newUserModel, false);
                    return RedirectToAction("Index", "HomePage");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(newUser);


        }
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(loginViewModel user)
        {
            ApplicationUser foundedUser = await userManager.FindByNameAsync(user.UserName);
            if (foundedUser != null)
            {
                bool result = await userManager.CheckPasswordAsync(foundedUser, user.Password);
                if (result == true)
                {
                    await signInManager.SignInAsync(foundedUser, user.RememberMe);
                    return RedirectToAction("Index", "HomePage");

                }
            }
            ModelState.AddModelError(string.Empty, "User name & password is in correct");
            return View(user);

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel newUser)
        {
            if (ModelState.IsValid == false)
            {
                return View(newUser);
            }
            try
            {
                ApplicationUser newUserModel = new ApplicationUser();
                newUserModel.UserName = newUser.UserName;
                newUserModel.Address = newUser.Address;
                newUserModel.PasswordHash = newUser.Password;
                newUserModel.Image = newUser.Image;
                IdentityResult result = await userManager.CreateAsync(newUserModel, newUser.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUserModel, "Admin");
                    await signInManager.SignInAsync(newUserModel, false);
                    
                    return RedirectToAction("Index", "HomePage");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(newUser);


        }

    }
}
