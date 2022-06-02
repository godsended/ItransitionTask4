using ItransitionTask4.Models;
using ItransitionTask4.Models.ViewModels;
using ItransitionTask4.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItransitionTask4.Controllers;

public class AccountController : Controller
{
    private UserContext userDatabase;
    private IAuth auth;
    private IActions actions;

    public AccountController(UserContext context, IAuth auth, IActions actions)
    {
        this.auth = auth;
        this.userDatabase = context;
        this.actions = actions;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            string key = await auth.Login(model.Email, model.Password);
            if (key!="")
            {
                Response.Cookies.Append("LoginKey", key);
                Response.Cookies.Append("Email", model.Email);
                actions.InvokeAnyUserAction();
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Incorrect email or password or your account is blocked");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            string key = await auth.Register(model.Name, model.Email, model.Password);
            if (key!="")
            {
                Response.Cookies.Append("LoginKey", key);
                Response.Cookies.Append("Email", model.Email);
                actions.InvokeAnyUserAction();
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Mail already registered");
        }
        
        return View(model);
    }
}