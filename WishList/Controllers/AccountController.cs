using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> sigInManager)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ret = _userManager.CreateAsync(new ApplicationUser { Email = model.Email, UserName = model.Email }, model.Password).Result;

            if (!ret.Succeeded)
            {
                foreach (var item in ret.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ret = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).Result;

            if (!ret.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");

                return View(model);
            }


            return RedirectToAction("Index", "Item");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {

            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
