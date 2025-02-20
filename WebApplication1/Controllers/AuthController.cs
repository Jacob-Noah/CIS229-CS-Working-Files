using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string usernameInput, string passwordInput)
    {
        string printMessage;
        if (usernameInput == "jacob" && passwordInput == "123")
        {
            printMessage = "Authenticated";
        }
        else
        {
            printMessage = "Unauthenticated"; 
        }

        ViewData["Message"] = printMessage;
        return View("Login");
    }

    public ActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public ActionResult ForgotPassword(string emailInput)
    {
        ViewData["Message"] = "Password is reset";
        return View("ForgotPassword");
    }
}