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
                dto.HasSideBar = model.HasSideBar;
                dto.Sorting = 100;

                //Сохраняем модель в базу данных
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            //Передаем сообщение через TempData
            TempData["Successful message"] = "You have added a new page";

            //Переадресовываем пользователя на метод INDEX
            return RedirectToAction("Index");
        }

        //GET: Admin/Pages/EditPage/id 
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            //Объявляем модель PageVM
            PageVM model;

            using(Db db = new Db ())
            {        
                //Получаем страницу
                PagesDTO dto = db.Pages.Find(id);

                //Проверяем, доступна ли страница
                if (dto == null)
                {
                    return Content("The page does not exist.");
                }
                //Eсли страница доступна, инициализириуем данные из DTO
                model = new PageVM(dto);
            }

            //Возвращаем модель в представление
            return View(model);
        }

        //POST: Admin/Pages/EditPage 
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            //Проверить модель на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                //Получаем ID страницы
                int id = model.Id;

                //Объявляем переменную краткого заголовка
                string slug = "home";

                //Получаем страницу (по ID)
                PagesDTO dto = db.Pages.Find(id);

                //Присваиваем название из полученной модели в DTO
                dto.Title = model.Title;

                //Проверка краткий заголовок (Slug) и присваиваем его, если это необходимо
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }

                //Проверяем краткий заголовок и заголовок на уникальность
                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title is already exist");
                    return View (model);
                }
                else if (db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That slug already exist.");
                    return View(model);
                }

                //Записываем остальные значения в класс DTO
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;

                //Сохраняем изменения в базу
                db.SaveChanges();
            }

            //Устанавливаем сообщение в TempData
            TempData["Successful message"] = "You have edited the page.";

            //Переадресация пользователя
            return RedirectToAction("EditPage");
        }

        //Get: Admin/Pages/PagesDetails/id  
        public ActionResult PageDetails(int id)
        {
            //Обявляем модель PageVm
            PageVM model;

            using (Db db = new Db())
            {
                //Подтверждаем, что страница доступна
                PagesDTO dto = db.Pages.Find(id);
                if (dto == null)
                {
                    return Content("The page does not exist.");
                }
                   
                //Присваеваем модели иннформацию из базы
                model = new PageVM(dto);
            }

            //Возвращаем модель представления            
            return View(model);
        }

        //создаем метод удаления
        //GET: Admin/Pages/DeletePage/id  
        public ActionResult DeletePage(int id)
        {
            using (Db db = new Db())
            {
                //Получаем страницу
                PagesDTO dto = db.Pages.Find(id);
                //Удаляем страницу
                db.Pages.Remove(dto);
                //Сохраняем изменения в базе
                db.SaveChanges();
            }

            //Добавляем сообщение о удачном удалении страницы
            TempData["Successful message"] = "You have deleted a page!";
            
            //Переадресовываем пользователя
            return RedirectToAction("Index");
        }

        
        //POST: Admin/Pages/ReorderPages  
        [HttpPost]
        public void ReorderPages(int [] id)
        {
            using (Db db = new Db())
            {            
                //Реализуем начальный счетчик
                int count = 1;

                //Инициализируем модель данных
                PagesDTO dto;

                //Устанавливаем сортировку для каждой страницы
                foreach (var pageId in id)
                {
                    dto = db.Pages.Find(pageId);
                    dto.Sorting = count;
                    db.SaveChanges();
                    count++;
                }
            }
        }


        //GET: Admin/Pages/EditSidebar  
        [HttpGet]
        public ActionResult EditSidebar()
        {
            //Объявляем модель
            SidebarVM model;

            using (Db db = new Db())
            {
                //Получаем данные из DTO
                SidebarDTO dto = db.Sidebars.Find(1); // Жесткое значение в коде не желательно добавлять, исправить. 
                               
                //Заполняем модель данными
                model = new SidebarVM(dto);
            }

            //Вернуть представление с моделью
            return View(model);
        }


        //POST: Admin/Pages/EditSidebar
        [HttpPost]
        public ActionResult EditSidebar(SidebarVM model)
        {
            using (Db db = new Db())
            {
                //Получить данные из бд
                SidebarDTO dto = db.Sidebars.Find(1); // Жесткое значение в коде не желательно добавлять, исправить. 
            
            
                //Присвоить данные в тело (в свойство Body)
                dto.Body = model.Body;

                //Сохраняем
                db.SaveChanges();
            }
            //Присваиваем сообщение в TempData
            TempData["Successful message"] = "You have edited the sidebar!";

            //Переадресовываем пользователя
            return RedirectToAction("EditSidebar");
        }
    }
}