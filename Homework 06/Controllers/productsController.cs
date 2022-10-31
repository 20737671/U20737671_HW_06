using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework_06.Models;
using PagedList.Mvc;
using PagedList;




namespace Homework_06.Controllers
{
    public class productsController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: products
        public ActionResult Index(string SearchString, int? i)
        {


            SearchString = (SearchString == null) ? "" : SearchString;

            ViewData["CurrentFilter"] = SearchString;

            var products = db.products.Include(p => p.brand).Include(p => p.category).OrderByDescending(p => p.product_name.Contains(SearchString));



            return View(products.ToList().ToPagedList(i ?? 1, 10));
            //return View(products.ToList());

        }






        // GET: products/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    product product = db.products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // GET: products/Create
        //public ActionResult Create()
        //{
        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
        //    return View();
        //}

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
        //    return View(product);
        //}

        // GET: products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    product product = db.products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
        //    return View(product);
        //}

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
        //    return View(product);
        //}

        // GET: products/Delete/5
        //public ActionResult Delete(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    product product = db.products.Find(id);
        ////    if (product == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    return View(product);
        ////}

        //// POST: products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    product product = db.products.Find(id);
        //    db.products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}



        public ActionResult Details(int? id)
        {

            if (Request.IsAjaxRequest())
            {
                product product = db.products.Find(id);
                if (product != null)
                {
                    return PartialView(product);
                }
                else
                {
                    Response.StatusCode = 403;
                    return PartialView("Error");
                }
            }
            Response.StatusCode = 500;
            return PartialView("Error");


        }





        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///





        private BikeStoresEntities _context;
        public productsController()
        {
            _context = new BikeStoresEntities();
        }
        
        public JsonResult List()
        {
            return Json(_context.Product.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(product user)
        {
            _context.Product.Add(user);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.Product.FirstOrDefault(x => x.product_id == ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(product user)
        {
            var data = _context.Product.FirstOrDefault(x => x.product_id == user.product_id);
            if (data != null)
            {
                data.product_name = user.product_name;
                data.model_year = user.model_year;
                data.list_price = user.list_price;
                data.brand = user.brand;
                data.category = user.category;
                _context.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            var data = _context.Product.FirstOrDefault(x => x.product_id == ID);
            _context.Product.Remove(data);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }




    }
}
