using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using my_store_project.Models.Data;
using my_store_project.Models.ViewModels.Cart;

namespace my_store_project.Controllers
{    
    // --20--
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            // Объявляем List типа CartVM
            var cart = Session["cart"] as List<CartVM>  ?? new List<CartVM>();

            // Проверяем, пуста ли корзина
            if (cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "Your cart is empty.";
                return View();
            }

            // Складываем сумму и записываем во ViewBag
            decimal total = 0m;

            foreach (var item in cart)
            {
                total += item.Total;
            }

            ViewBag.GrandTotal = total;

            // Возвращаем List в представление
            return View(cart);
        }
        public ActionResult CartPartial()
        {
            // Объявляем модель CartVM
            CartVM model = new CartVM();           

            // Объявляем переменную количества
            int qty = 0;

            // Объявляем переменную цены
            decimal price = 0m;

            // Проверяем сессию корзины
            if (Session["cart"] != null)
            {
                // получаем общее количество товаров и цену
                var list = (List<CartVM>)Session ["cart"];

                foreach (var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }
            }
            else
            {
                // или устанавливаем количество и цену в 0
                model.Quantity = 0;
                model.Price = 0m;
            }           
            
            // вернуть частичное представление с моделью           

            return PartialView("_CartPartial", model);
        }

        //21
        public ActionResult AddToCartPartial(int id)
        {
            // Объявляем лист, параметризированный типом CartVM

            
            // Объявляем модель CartVM

            
            // Получаем продукт

            
            // Проверяем, находится ли товар уже в корзине

            
            // Если нет, то добавляем товар в корзин

            
            // Если да, добавляем еденицу товара

            
            // Получаем общее количество, цену и добавляем данные в модель

            
            // сохраняем состояние корзины в сессию

            
            // Возвращаем частичное представление с моделью

            


            return View();
        }
    }
}