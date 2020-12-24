using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Models;

namespace WebShopping.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSecession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = (List<CartItem>)(null);
            if (cart != null)
            {
                list = (List<CartItem>)cart;

            }
            return View(list);
        }
        public ActionResult AddItem(int productID,int quantiy)
        {
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list=(List<CartItem>)cart;
                if (list.Exists(x => x.ProducID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.ProducID == productID)
                        {
                            item.Quantity += quantiy;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng
                    var item = new CartItem();
                    item.ProducID = productID;
                    item.Quantity = quantiy;
                    list.Add(item);
                }
                //gán vào Session
                Session[CartSession] = list;

            }
            else
            {
                //tạo mới đối tượng
                var item = new CartItem();
                item.ProducID = productID;
                item.Quantity = quantiy;
                var list = new List<CartItem>();

                //gán vào Session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}