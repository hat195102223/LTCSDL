using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTCSDL.Models;

namespace LTCSDL.Controllers
{
    public class CartController : Controller
    {
        private DataClasses1DataContext dt = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<Cart> GetListCarts()
        {
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts == null)
            {
                carts = new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }
    }
}