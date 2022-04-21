using ecommerceProjectMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ecommerceProjectMVC.Controllers
{
    //[Authorize (Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(RoleViewModel newRole)
        {

            if (ModelState.IsValid == false)
            {
                return View(newRole);
            }
            IdentityRole RoleModel = new IdentityRole(newRole.RoleName);
            IdentityResult result = await roleManager.CreateAsync(RoleModel);
            if (result.Succeeded)
            {
                return View(new RoleViewModel());
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(newRole);


        }

    }
}
