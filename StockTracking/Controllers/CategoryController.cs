using StockTracking.Models.Entities;
using StockTracking.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.Controllers
{

    [Authorize(Roles = "Admin,User")]

    public class CategoryController : Controller
    {
        
        // GET: Category
        StockTrackingEntities context = new StockTrackingEntities();

       
        public ActionResult Index()
        {

            return View(context.Category.ToList());

        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            context.Category.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GetCategory(int id)
        {
            var data = context.Category.Find(id);
            MyCategory category = new MyCategory();
            category.CategoryId = data.CategoryId;
            category.Category1 = data.Category1;
            category.Description = data.Description;

            if (data == null)
            {
                return HttpNotFound();
            }
            return View("GetCategory", category);
        }
        public ActionResult Update(Category category)
        {
            context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult GetRemove(int id)
        {
            var data = context.Category.Find(id);
            if (data == null) return HttpNotFound();
          
            return View("GetRemove", data);
        }
        public ActionResult Remove(Category category)
        {
            var data = context.Category.Find(category.CategoryId);
            context.Category.Remove(data);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}