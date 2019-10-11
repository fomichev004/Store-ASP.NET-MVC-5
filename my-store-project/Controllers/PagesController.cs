using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web;
using System.Web.Mvc;
using my_store_project.Models.ViewModels.Pages;

namespace my_store_project.Controllers
{    
    public class PagesController : Controller
    {    
        //GET:Index/{page}        
        public ActionResult Index(string page = "")
        {
            //Получаем/устанавливаем краткий заголовок (SLUG)
            if (page = "")
                page = "home";

            //Объявляем модель и класс DTO
            PageVM model;
            PagesDTO dto;

            //Проверяем, доступна ли страница
            using (Db db = new Db())
            {
                if (!db.Pages.Any(x => x.Slug.Equals(pages)))
                return RedirectToAction("Index", new {page = ""});
            }

            //Получаем DTO страницы
            using (Db db = new Db())
            {
                dto = db.Pages.Where( x => x.Slug == page).FirstOrDefault();
            }

            //Устанавливаем заголовок страницы (TITLE)
            ViewBag.PageTitle = dto.Title;

            //Проверяем боковую панель
            if (dto.Hassidebar == true)
            {
                ViewBag.Sidebar = "Yes";
            }
            else
            {
                ViewBag.Sidebar = "No";
            }

            //Заполняем модель данными
            model = new PageVM(dto);

            //Возвращаем представление с моделью
            return View(model);
        }
        
        public ActionResult PagesMenuPartial()
        {
            // Инициализируем тип лист PageVM
            List<PageVM> PageVMList;

            //Получаем все страницы, кроме HOME
            using (Db db = new Db())
            {
                PageVMList = db.Pages.ToArray().OrderBy( x=> x.Sorting).Where( x => x.Slug != "home").Select(x => new PageVM(x)).ToList();
            }
            // Возвращаем частичное представление с листом ланных
            return PartialView("_PagesMenuPartial", pageVMList);
        }
    }    
}
