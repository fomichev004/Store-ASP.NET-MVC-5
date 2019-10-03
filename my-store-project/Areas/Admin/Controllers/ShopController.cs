﻿using my_store_project.Models.Data;
using my_store_project.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.IO;
using System.Web.Mvc;

namespace my_store_project.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop
        public ActionResult Categories()
        {
        	//Объявляем модель типа List
        	List<CategoryVM> categoryVMList;
        	using (Db db = new Db())
        	{        	
        		//Инициализируем модель данными
        		categoryVMList = db.Categories.ToArray().OrderBy( x=> x.Sorting).Select( x=> new CategoryVM(x)).ToList();
        	}

        	//Возвращаем List в представление
            return View(categoryVMList);
        }

        //POST:Admin/Shop/AddNewCategory
        [HttpPost]
        public string AddNewCategory(string catName)
        {
        	// Объявляем строковою переменную ID
        	string id;

        	using (Db db = new Db())
        	{
        		//Проверяем имя категории на уникальность
        		if (db.Categories.Any(x => x.Name == catName))
                    return "titletaken"; 		

	        	//Инициализируем модель DTO
	        	CategoryDTO dto = new CategoryDTO();

	        	//Добавляем данные в модель
	        	dto.Name = catName;
	        	dto.Slug = catName.Replace(" ", "-").ToLower();
	        	dto.Sorting = 100;

	        	//Сохранить
	        	db.Categories.Add(dto);
	            db.SaveChanges();

	        	//Получаем ID для возврата в представлениие
	        	id = dto.Id.ToString();
        	}
        	//Возвращаем ID в представление
        	return id;
        }

        //POST: Admin/Shop/ReorderCategories  
        [HttpPost]
        public void ReorderCategories(int[] id)
        {
            using (Db db = new Db())
            {
                //Реализуем начальный счетчик
                int count = 1;

                //Инициализируем модель данных
                CategoryDTO dto;

                //Устанавливаем сортировку для каждой страницы
                foreach (var catId in id)
                {
                    dto = db.Categories.Find(catId);
                    dto.Sorting = count;
                    db.SaveChanges();
                    count++;
                }
            }
        }

        //GET: Admin/Shop/DeleteCategory/id  
        public ActionResult DeleteCategory(int id)
        {
            using (Db db = new Db())
            {
                //Получаем модель категории
                CategoryDTO dto = db.Categories.Find(id);

                //Удаляем категорию
                db.Categories.Remove(dto);

                //Сохраняем изменения в базе
                db.SaveChanges();
            }

            //Добавляем сообщение о удачном удалении категории
            TempData["Successful message"] = "You have deleted a category!";

            //Переадресовываем пользователя
            return RedirectToAction("Categories");
        }

        //POST: Admin/Shop/RenameCategory/id  
        [HttpPost]
        public string RenameCategory(string newCatName, int id)
        {
            using (Db db = new Db())
            {
                //Проверяем имя на уникальность
                if (db.Categories.Any(x => x.Name == newCatName))
                    return "titletaken";

                //Получаем модель DTO
                CategoryDTO dto = db.Categories.Find(id);

                //Редактируем модель DTO
                dto.Name = newCatName;
                dto.Slug = newCatName.Replace(" ", "-").ToLower();


                //Сохраняем изменение
                db.SaveChanges();
            }
            //Возвращаем слово
            return "ok";
        }

        //Создаметод добавления товаров
        //GET: Admin/Shop/AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            //Объявляем модел
            ProductVM model = new ProductVM();

            //Добавляем список категорий из базы в модель
            using (Db db = new Db())
            {
                model.Categories = new SelectList(db.Categories.ToList(), "id", "Name");
            }
            //Возвращаем модель в представление
            return View(model);
        }

        //Создаметод добавления товаров ------------------------------------------------
        //POST: Admin/Shop/AddProduct
        [HttpPost]
        public ActionResult AddProduct(ProductVM model, HttpPostedFileBase file) {
            //Проверка модели на валидность
            if (!ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    return View(model);
                }
            }

            //Проверяем имя продукта на уникальность
            using (Db db = new Db())
            {
                if (db.Products.Any(x=> x.Name == model.Name))
                {
                        model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        ModelState.AddModelError("", "That product name is taken!");
                        return View(model);


                }
            }

            // Объявляем переменную ProductId
            int id;
            //Инициализируем и сохраняем модель на основе ProductDTO
            using (Db db = new Db())
            {
                ProductDTO product = new ProductDTO();

                product.Name = model.Name;
                product.Slug = model.Name.Replace(" ", "-").ToLower();
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;

                CategoryDTO catDTO = db.FirstOrDefault( x => x.Id == model.CategoryId);
                product,CategoryName = catDTO.Name;

                db.Product.Add(product);
                db.SaveChanges();

                id = product.Id;
            }
                //Добавляем сообщение в TempData
            	TempData["Successful message"] = "You have added a product!";

                #region Upload Image

                //Создаем необходимые ссылки на дериктории
                var originalDirectory = new DirectoryInfo (string.Format($"{Server.MapPath(@"\")}Images\\Uploads"));

                var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
                var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\ + id.ToString() + \\Thumbs");
                var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\ + id.ToString() + \\Gallery");
                var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\ + id.ToString() + \\Gallery\\Thumbs");

                //Проверяем наличие директорий (если нет, создаём)
                if (!Directory.Exists(pathString1))
               		Directory.CreateDirectory(pathString1);

               	if (!Directory.Exists(pathString2))
               		Directory.CreateDirectory(pathString2);

               	if (!Directory.Exists(pathString3))
               		Directory.CreateDirectory(pathString3);

               	if (!Directory.Exists(pathString4))
               		Directory.CreateDirectory(pathString4);

               	if (!Directory.Exists(pathString5))
               		Directory.CreateDirectory(pathString5);

                //Проверяем, был ли файл загружн
               	if (file != Null && file.ContentLenght > 0)
               	{
               		//Получаем расширение файла
               		string ext = file.ContentType.ToLower();

               		//Проверяем расширение файла
               		if (ext != "image/jpg" && 
               			ext != "image/jepg" &&
               			ext != "image/pjepg" &&
               			ext != "image/gif" &&
               			ext != "image/png" &&
               			ext != "image/x-png")
               		{
               			using (Db db = new Db())
               			{
               				model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name")
               				ModelState.AddModelError("", "The image was not uploaded - wrong image extentsion!")
               				return View(model);
               			}
               		}
               	
                
	                //Объявляем переменную с именем изображения
	                string imageName = file.FileName;

	                //Сохраняем имя изображения в модель DTO
	                using (Db db = new Db())
	                {
	                	ProductDTO dto = db.Products.Find(id);
	                	dto.ImageName = imageName;

	                	db.SaveChanges();
	                }

	                //Назначаем пути к оригинальному и уменьшенному изображению
	                var path = string.Format($"{pathString2}\\{imageName}"); //путь к оригинальному изображению
	                var path2 = string.Format($"{pathString3}\\{imageName}"); //путь к уменьшенному изображению

	                //Сохраняем оригинальное изображение
	                file.SaveAs(path);

	                //Создаем и сохраняем уменьшенную копию
	                WebImage img = new WebImage(file.InputStream);
	                img.Resize(200, 200);
	                img.Save(path2);
       			 }

                #endregion
                // переадресовываем пользователя
                return RedirectToAction("AddProduct");          
        }




        //                //Присваеваем заголовок модели
        //                dto.Title = model.Title.ToUpper();

        //                //Проверяем, есть ли краткое описание, если нет, присваиваем его
        //                if (string.IsNullOrWhiteSpace(model.Slug))
        //                {
        //                    ProductId = model.Title.Replace(" ", "-").ToLower();
        //                }
        //                else
        //                {
        //                    ProductId = model.ProductId.Replace(" ", "-").ToLower();
        //                }
        //                //Убеждаемся, что заголовок и краткое описание - уникальны
        //                if (db.Pages.Any(x=> x.Title == model.Title))
        //                {
        //                    ModelState.AddModelError("", "That title already exist");
        //                    return View(model);
        //                }
        //                else if (db.Pages.Any(x=> x.Slug == model.Slug))
        //                {
        //                    ModelState.AddModelError("", "That slug already exist");
        //                    return View(model);
        //                }

        //                //Присваиваем оставшиеся значения модели
        //                dto.ProductId = ProductId;
        //                dto.Body = model.Body;
        //                dto.HasSideBar = model.HasSideBar;
        //                dto.Sorting = 100;

        //                //Сохраняем модель в базу данных
        //                db.Pages.Add(dto);
        //                db.SaveChanges();
        //            }

        //            //Передаем сообщение через TempData
        //            TempData["Successful message"] = "You have added a new page";

        //            //Переадресовываем пользователя на метод INDEX
        //            return RedirectToAction("Index");
        //        }

    }
}