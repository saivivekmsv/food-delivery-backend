using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

public class OperationsController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    public OperationsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public ViewResult Create() => View();
    public ViewResult CreateRole() => View();
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if(ModelState.IsValid)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = user.Name,
                Email = user.Email
            };
            IdentityResult result = await _userManager.CreateAsync(applicationUser,user.Password);
            await _userManager.AddToRoleAsync(applicationUser,user.RoleName);
            if (result.Succeeded)
                    ViewBag.Message = "User Created Successfully";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
        }
            return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([Required]string name)
    {
        if(ModelState.IsValid)
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole(){Name=name});
            if (result.Succeeded)
                    ViewBag.Message = "Role Created Successfully";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
        }
            return View();
        
    }
}


