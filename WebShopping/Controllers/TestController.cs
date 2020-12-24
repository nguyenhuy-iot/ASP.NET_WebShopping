using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Models;

namespace WebShopping.Controllers
{
    public class TestController : Controller
    {
        private ShopContext db = new ShopContext();
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}