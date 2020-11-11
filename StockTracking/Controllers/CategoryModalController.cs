using Newtonsoft.Json;
using StockTracking.Models.Entities;
using StockTracking.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.Controllers
{
    public class CategoryModalController : Controller
    {
        // GET: CategoryModal
        StockTrackingEntities c = new StockTrackingEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCategoryList()
        {
            List<MyCategory> list = c.Category.Select(x => new MyCategory
            {
                CategoryId=x.CategoryId,
                Category1=x.Category1,
                Description=x.Description
            }).ToList();
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            c.Entry(category).State = System.Data.Entity.EntityState.Added;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(Category category)
        {
            c.Entry(category).State = System.Data.Entity.EntityState.Modified;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            c.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult GetEditDeleteCategory(int ID)
        {
            var model = c.Category.FirstOrDefault(x=>x.CategoryId==ID);
            string value = "";
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {

                ReferenceLoopHandling=ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}