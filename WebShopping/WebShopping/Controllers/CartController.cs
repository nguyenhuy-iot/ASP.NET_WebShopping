using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebShopping.Models;

namespace WebShopping.Controllers
{
    public class CartController : Controller
    {
        private ShopContext db = new ShopContext();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = db.Product.Find(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ProductID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ProductID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Update(FormCollection form )
        {
            int productId = int.Parse(form["ProductID"]);
            int Q_ty = int.Parse(form["Quantity"]);

            var product = db.Product.Find(productId);
            var cart = Session[CartSession];

            var list = (List<CartItem>)cart;
            if (list.Exists(x => x.Product.ProductID == productId))
            {

                foreach (var item in list)
                {
                    if (item.Product.ProductID == productId)
                    {
                        item.Quantity = Q_ty;
                    }
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult DeleteAll()
        {
            Session[CartSession] = null;
            var list = new List<CartItem>();
            //return View("Index", list);
            return RedirectToAction("Index");
        }
    }
}