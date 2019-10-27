using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using my_store_project.Models.Data;
using my_store_project.Models.ViewModels.Account;

namespace my_store_project.Controllers
{    
    // --23--
    public class AccountController : Controller
    {
        //GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: account/create-account        
        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        //GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            // подтвердить, что пользователь не авторизован
            string userName = User.Identity.Name;
            
            if (!string.IsNullOrEmpty(userName))
                return RedirectToAction("user-profile");

            //возвращаем представление
            return View();
        }
        
    }
}