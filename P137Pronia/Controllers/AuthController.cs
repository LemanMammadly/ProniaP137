using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P137Pronia.Models;
using P137Pronia.ViewModels.AuthVMs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P137Pronia.Controllers
{
    public class AuthController : Controller
    {
        readonly UserManager<ApppUser> _userManager;
        readonly SignInManager<ApppUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApppUser> userManager, SignInManager<ApppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; 
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if(!ModelState.IsValid) return View();
            ApppUser user = new ApppUser
            {
                Fullname=vm.Name+ " " + vm.Surname,
                Email=vm.Email,
                UserName=vm.Username
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            var res = await _userManager.AddToRoleAsync(user, "Member");

            if(!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? ReturnUrl,LoginVM vm)
        {
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            if(user==null)
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
                if(user==null)
                {
                    ModelState.AddModelError("", "Username,email or password is wrong");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user,vm.Password,vm.RememberMe,true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Wait until " + user.LockoutEnd.Value.AddHours(4).ToString("HH:mm:ss"));
                return View();
            }
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username,email or password is wrong");
                return View();
            }
            if (ReturnUrl == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(ReturnUrl);
            }
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}

