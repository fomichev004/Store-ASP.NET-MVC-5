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

                model.Quantity = qty;
                model.Price = price;
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
            List<CartVM> cart = Session["cart"] as List<CartVM> ?? new List<CartVM>();
            
            // Объявляем модель CartVM
            CartVM model = new CartVM();            
            
            using (Db db = new Db())
            {
                // Получаем продукт
                ProductDTO product = db.Products.Find(id);

                // Проверяем, находится ли товар уже в корзине
                var productInCart = cart.FirstOrDefault(x => x.ProductId == id);
            
                // Если нет, то добавляем товар в корзин
                if (productInCart == null)
                {
                    cart.Add(new CartVM()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = 1,
                        Price = product.Price,
                        Image = product.ImageName
                    });
                }
                // Если да, добавляем еденицу товара
                else
                {
                    productInCart.Quantity++;
                }
            }            
            // Получаем общее количество, цену и добавляем данные в модель
            int qty = 0;
            decimal price = 0m;

            foreach (var item in cart)
            {
                qty += item.Quantity;
                price += item.Quantity * item.Price;
            }
            
            model.Quantity = qty;
            model.Price = price;

            // сохраняем состояние корзины в сессию
            Session["cart"] = cart;
            
            // Возвращаем частичное представление с моделью      
            return PartialView("_AddToCartPartial", model);
        }
        
        // 21
        //GET /cart/IncrementProduct
        public JsonRult IncrementProduct(int productId)
        {
            // Объявляем List cart
            List<CartVM> cart = Session["cart"] as List<CartVM>;
            
            using (Db db = new Db())
            {            
                // получаем модель CartVM из листа
                CartVM model = cart.FirstOrDefault(x => x.ProductId == productId);

                // добавляем количество
                model.Quantity++;

                // сохраняем необходимые еданные
                var result = new { qty = model.Quantity, price = model.Price};

                // Возвращаем JSON ответ с данными
                return Json(result, JsonRequestBehavior.AllowGet);
            }  
        }
    }
}