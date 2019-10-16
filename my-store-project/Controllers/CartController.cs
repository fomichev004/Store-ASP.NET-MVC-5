using System;
using System.Collections.Generic; 
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using my_store_project.Models.ViewModels.Cart;
using my_store_project.Models.Data;

namespace my_store_project.Controllers
{    
    // --20--
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

            return CartPartialView();
        }
    }
}