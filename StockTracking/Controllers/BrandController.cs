using StockTracking.Models.Entities;
using StockTracking.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.Controllers
{
    [Authorize(Roles="Admin,User")]
    public class BrandController : Controller
    {
        // GET: Brand
        StockTrackingEntities c = new StockTrackingEntities();
        public ActionResult Index()
        {
            var data = c.Brand.ToList();
            return View(data);
        }
        public ActionResult Add()
        {
            var data = new Brand();
            List<SelectListItem> list = new List<SelectListItem>(from x in c.Category
                                                                 select new SelectListItem
            {
                Value=x.CategoryId.ToString(),
                Text=x.Category1
            }).ToList();
            ViewBag.Cat = list;
            return View();
        }
        private void Reload()
        {
            var model = new MyBrand();
            List<SelectListItem> list = new List<SelectListItem>(from x in c.Category
                                                                 select new SelectListItem
                                                                 {
                                                                     Value=x.CategoryId.ToString(),
                                                                     Text=x.Category1
                                                                 }).ToList();
            ViewBag.list = list;
        }
        [HttpPost]
        public ActionResult Add(Brand brand)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.CategoryId = new SelectList(c.Category,"CategoryId","Category1",brand.CategoryId);

                return View();
            }
            c.Brand.Add(brand);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetUpdate(int id)
        {
            MyBrand model = new MyBrand();
            List<SelectListItem> list = new List<SelectListItem>(from x in c.Category
                                                                 select new SelectListItem
                                                                 {
                                                                     Value = x.CategoryId.ToString(),
                                                                     Text = x.Category1
                                                                 }).ToList();
            ViewBag.Cat = list;
            var data = c.Brand.Find(id);
            model.BrandId = data.BrandId;
            model.Brand1 = data.Brand1;
            model.Description = data.Description;
            return View("GetUpdate",model);
        }
        public ActionResult Update(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> list = new List<SelectListItem>(from x in c.Category
                                                                     select new SelectListItem
                                                                     {
                                                                         Value = x.CategoryId.ToString(),
                                                                         Text = x.Category1
                                                                     }).ToList();
                ViewBag.Cat = list;
                return View("GetUpdate");
            }
            c.Entry(brand).State = System.Data.Entity.EntityState.Modified;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetRemove(int id)
        {
            var data = c.Brand.Find(id);
            
            return View("GetRemove",data);
        }
        public ActionResult Remove(Brand brand)
        {
            c.Entry(brand).State = System.Data.Entity.EntityState.Deleted;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}