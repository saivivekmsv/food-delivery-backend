﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

public class AccountController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Login()
{
    return View();
}
 
[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string? returnurl)
{
    if(ModelState.IsValid)
    {
        ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
        if (appUser != null)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnurl ?? "/");
            }
        }
        ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
    }
 
    return View();
}
[Authorize]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
}

}
