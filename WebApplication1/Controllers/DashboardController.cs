using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccessLayer;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class DashboardController : Controller
{
    public ActionResult Index()
    {
        UserDataAccess dataAccess = new UserDataAccess();
        List<UserModel> userList=dataAccess.GetUserList();

        return View(userList);
    }
}