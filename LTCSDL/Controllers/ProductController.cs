using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTCSDL.Models;

namespace LTCSDL.Controllers
{
    public class ProductController : Controller
    {

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        DataClasses1DataContext dt = new DataClasses1DataContext();
        public ActionResult ProductView()
        {
            var p = dt.Products.Select(s => s);
            return View(p);
        }
        public ActionResult ProductDetails(int id)
        {
            var p = dt.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }
        public ActionResult Create()
        {
            ViewData["NCC"] = new SelectList(dt.Suppliers, "SupplierID", "CompanyName");
            ViewData["LoaiSP"] = new SelectList(dt.Categories, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, Product pro)
        {
            var tenSP = form["ProductName"];
            if (string.IsNullOrEmpty(tenSP))
                ViewData["Loi"] = "Vui long nhap ten san pham";
            else
            {

                pro.CategoryID = int.Parse(form["LoaiSP"]);
                pro.SupplierID = int.Parse(form["NCC"]);
                dt.Products.InsertOnSubmit(pro);
                dt.SubmitChanges();
            }
            return RedirectToAction("ProductView");
        }
        public ActionResult Edit(int id)
        {
            var p = dt.Products.Where(s => s.ProductID == id).FirstOrDefault();
            ViewData["NCC"] = new SelectList(dt.Suppliers, "SupplierID", "CompanyName");
            ViewData["LSP"] = new SelectList(dt.Categories, "CategoryID", "CategoryName");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form, int id)
        {
            var p = dt.Products.FirstOrDefault(s => s.ProductID == id);

            p.ProductName = form["ProductName"];
            p.SupplierID = int.Parse(form["NCC"]);
            p.CategoryID = int.Parse(form["LoaiSP"]);
            p.QuantityPerUnit = form["QuantityperUnit"];
            p.UnitPrice = Decimal.Parse(form["UnitPrice"]);
            p.UnitsInStock = short.Parse(form["UnitsInStock"]);
            p.UnitsOnOrder = short.Parse(form["UnitsonOrder"]);
            p.ReorderLevel = short.Parse(form["ReorderLevel"]);
            p.Discontinued = bool.Parse(form["Discontinued"]);

            UpdateModel(p);
            dt.SubmitChanges();
            return RedirectToAction("ProductView");
        }

        public ActionResult Delete(int id)
        {
            var p = dt.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection f, int id)
        {
            var p = dt.Products.FirstOrDefault(s => s.ProductID == id);
            dt.Products.DeleteOnSubmit(p);
            dt.SubmitChanges();
            return RedirectToAction("ProductView");
        }
    }
}
