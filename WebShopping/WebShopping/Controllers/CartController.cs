using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            //var product = db.Product.Find(productId);
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
            //Session[CartSession] = null;
            //var list = new List<CartItem>();
            //return View("Index", list);

            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            list.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int productId)
        {            
            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;

            list.RemoveAll(s=>s.Product.ProductID== productId);

            return RedirectToAction("Index");
        }

        public PartialViewResult BagCart()
        {
            int _item = 0;
            var cart = Session[CartSession];
            if (cart!=null)
            {
                var list = (List<CartItem>)cart;
                _item = list.Sum(s => s.Quantity);
            }            
            ViewBag.infoCart = _item;
            return PartialView("BagCart");
        }
        [Authorize]
        public ActionResult Checkout()
        {
            //string UserID = User.Identity.GetUserId();
            //string UserName = User.Identity.GetUserName();  
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                //check Customer                
                string Email = User.Identity.GetUserName();
                int Customer_ID = 0;
                var Cus = from s in db.Customer
                               select s;
                var C_user = Cus.Where(s => s.Email == Email).ToList();            
                if (C_user.Count()>0)
                {
                    Customer_ID = C_user[0].ID;

                }
                else
                {
                    //Create Customer "ID,Name,Address,Phone,Email"
                    Customer customer = new Customer();
                    customer.Name = Email;
                    customer.Address = form["Ship_Address"];
                    customer.Phone = form["Phone"];
                    customer.Email = Email;
                    db.Customer.Add(customer);
                    Customer_ID = customer.ID;
                    //db.SaveChanges();
                }

                //Create Order "ID,CustomerID,OrderDate,RequreDate,ShipAddress,Phone"
                Order OrderItem = new Order();
                OrderItem.CustomerID = Customer_ID;
                OrderItem.OrderDate = DateTime.Now;
                OrderItem.RequreDate = DateTime.Parse(form["Requre_Date"]);
                OrderItem.ShipAddress = form["Ship_Address"];
                OrderItem.Phone = form["Phone"];
                db.Order.Add(OrderItem);
                //db.SaveChanges();
                //Create OrderDetail "OrderID,ProductID,Quantity,UnitPrice"
                var cart = Session[CartSession];
                var list = (List<CartItem>)cart;
                foreach (var item in list)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = OrderItem.ID;
                    orderDetail.ProductID = item.Product.ProductID;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.UnitPrice = item.Product.UnitPrice;
                    db.OrderDetail.Add(orderDetail);
                }
                db.SaveChanges();
                //Delete All Cart
                list.Clear();
                return View("Checkout_Success");
            }
            catch
            {
                return Content("Error Checkout. Kiểm tra lại thông tin");
                
            }
            
        }
    }
}