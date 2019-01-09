using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WishList.Models;
namespace WishList.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _sigInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> sigInManager)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
        }


    }
}
