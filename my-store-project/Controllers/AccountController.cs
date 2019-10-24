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
        public ActionResult Index()
        {
            return View();
        }

        // GET: account/create-account
        
        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }
    }
}