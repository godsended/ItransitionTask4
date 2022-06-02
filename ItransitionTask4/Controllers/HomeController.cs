using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ItransitionTask4.Models;
using ItransitionTask4.Models.ViewModels;
using ItransitionTask4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace ItransitionTask4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly UserContext userDatabase;
    private readonly IAuth auth;

    public HomeController(ILogger<HomeController> logger, UserContext userDatabase, IAuth auth)
    {
        this.logger = logger;
        this.userDatabase = userDatabase;
        this.auth = auth;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        HomeViewModel viewModel = new HomeViewModel();
        viewModel.Users = userDatabase.Users;
        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Index(HomeViewModel model)
    {
        List<int> ids = new List<int>();
        foreach (var key in Request.Form.Keys)
        {
            int.TryParse(Request.Form[key], out int id);
            {
                ids.Add(id);
            }
        }

        switch (Request.Form["action"])
        {
            case "BlockUsers":
                await userDatabase.BlockUsersByIds(ids);
                break;
            case "UnblockUsers":
                await userDatabase.UnblockUsersByIds(ids);
                break;
            case "DeleteUsers":
                await userDatabase.DeleteUsersByIds(ids);
                break;
        }

        string? loginKey = HttpContext.Request.Cookies["LoginKey"];
        string? email = HttpContext.Request.Cookies["Email"];

        await HttpContext.SignOutAsync();
        if (loginKey != null && email != null && await auth.TryAutoLogin(email, loginKey))
        {
            await auth.Authenticate(email);
        }
        else
        {
            HttpContext.Response.Cookies.Delete("LoginKey");
            HttpContext.Response.Cookies.Delete("Email");
            return RedirectToAction("Login", "Account");
        }
        model.Users = userDatabase.Users;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}