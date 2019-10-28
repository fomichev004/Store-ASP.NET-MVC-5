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

        // POST: account/create-account        
        [ActionName("create-account")]
        [HttpPost]
        public ActionResult CreateAccount(UserVM model)
        {
            //проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View("CreateAccount", model);
            }
            if(!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Password do not match");
                return View("CreateAccount", model);
            }

            //проверяем соответсвие пароля
            using (Db db = new Db())
            {
                //проверяем имя на уникальность
                if (db.Users.Any(x => x.Username.Equals(model.Username)))
                {
                    ModelState.AddModelError("", $"Username {model.Username} is taken.");
                    model.Username = "";
                    return View("CreateAccount", model);
                }

                //создаем экземпляр класса UserDTO
                UserDTO userDTO = new UserDTO()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailAddress = model.EmailAddress,
                    Username = model.Username,
                    Password = model.Password
                };

                //добавляем данные в модель
                db.Users.Add(userDTO);

                //сохраняем данные
                db.SaveChanges();

                //добавляем роль пользователю
                int id = userDTO.Id;
                UserRoleDTO userRoleDTO = new UserRoleDTO()
                {
                    UserId = id,
                    RoleId = 2
                };
                db.UserRoles.Add(userRoleDTO);
                db.SaveChanges();
            }

            //записать сообщение в TempData
            TempData["Successful message"] = "You are nowregistered and can login.";

            //переадресовываем пользователя
            return RedirectToAction("Login");
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

        //POST: Account/Login
        [HttpPost]
        public ActionResult Login(LoginUserVM model)
        {
            //проверяем модель на валидность
            //проверяем пользователя на валидность
            return View();
        }
    }
}