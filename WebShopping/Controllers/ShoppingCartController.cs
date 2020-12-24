using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Models;

namespace WebShopping.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index(int? id)
        {

            Models.ShopContext db = new Models.ShopContext();
            CatProViewModel cp = new CatProViewModel();

            if (id.HasValue)
            {
                var products = from p in db.Product
                               where p.Category.ID == id
                               select p;
                cp.Products = products.ToList<Product>();
                cp.Categories = db.Categories.ToList<Category>();

            }
            else
            {
                cp.Products = db.Product.ToList<Product>();
                cp.Categories = db.Categories.ToList<Category>();

            }
            return View(cp);
        }
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
            }
            cart.AddItem(id.ToString(), quantity, 300);
            //Save cart
            Session["cart"] = cart;
            return RedirectToAction("YourCart");
        }
        public ActionResult YourCart()
        {
            //Models.NorthwindEntities db = new Models.NorthwindEntities();
            //return View(db.Categories.Include("Products").ToList());
            return View();
        }
        public ActionResult Category(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Models.ShopContext db = new Models.ShopContext();
            var categories = from c in db.Categories.Include("Products")
                             where c.ID == id
                             select c;

            return View(categories.ToList());
        }
        public ActionResult RemoveItem(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
            }
            cart.RemoveItem(id.ToString());
            return RedirectToAction("YourCart");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

    }
}