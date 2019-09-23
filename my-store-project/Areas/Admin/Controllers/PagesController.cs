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

        // GET: Admin/Pages/AddPage
         [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // POST: Admin/Pages/AddPage
        [HttpPost]
         public ActionResult AddPage(PageVM model)
        {
            //Проверка модели на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                //Объявляем переменную для краткого описания (slug)
                string slug;

                //Инициализируем класс PageDTO
                PagesDTO dto = new PagesDTO();

                //Присваеваем заголовок модели
                dto.Title = model.Title.ToUpper();

                //Проверяем, есть ли краткое описание, если нет, присваиваем его
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }
                //Убеждаемся, что заголовок и краткое описание - уникальны
                if (db.Pages.Any(x=> x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist");
                    return View(model);
                }
                else if (db.Pages.Any(x=> x.Slug == model.Slug))
                {
                    ModelState.AddModelError("", "That slug already exist");
                    return View(model);
                }


                //Присваиваем оставшиеся значения модели
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSlideBar = model.HasSlideBar;
                dto.Sorting = 100;

                //Сохраняем модель в базу данных
                db.Pages.Add(dto);
                db.SaveChanges();

            }

            //Передаем сообщение через TempData (sm?)
            TempData["Successful message"] = "You have added a new page";

            //Переадресовываем пользователя на метод INDEX
            return RedirectToAction("Index");
        }

    }
}