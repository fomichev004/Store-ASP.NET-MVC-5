using my_store_project.Models.Data;
using my_store_project.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace my_store_project.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Объявляем спсок для представления (PageVM)
            List<PageVM> pageList;

            // Инициализируем список (DB)
            using (Db db = new Db())
           {
                pageList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
           }

            // Возвращаем представление 
            return View(pageList);
        }
    }
}