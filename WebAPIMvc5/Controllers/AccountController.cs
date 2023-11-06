using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPIMvc5.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (YourAuthenticationLogic(username, password))
            {
                var token = JwtManager.GenerateToken(username);
                return Json(new { token });
            }
            else
            {
                return Json(new { error = "Invalid username or password" });
            }
        }

        private bool YourAuthenticationLogic(string username, string password)
        {
            return (username == "admin" && password == "admin");
        }
    }
}