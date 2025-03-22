using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccessLayer;
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
        Boolean gotUser = false;
        UserDataAccess dataAccess = new UserDataAccess();
        gotUser = dataAccess.GetUser(usernameInput, passwordInput);

        if (gotUser)
        {
            ViewData["Message"] = "Authenticated";

            // if(RememberMe) 
            // {
            //     HttpCookie cookie = new HttpCookie("UserLogin", usernameInput);
            //     cookie.Expires = DateTime.Now.AddDays(7);
            //     Response.Cookies.Add(cookie);
            // }
        }
        else
        {
            ViewData["Message"] = "Unauthenticated";
        }

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