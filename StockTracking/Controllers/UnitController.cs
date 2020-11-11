using StockTracking.Models.Entities;
using StockTracking.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UnitController : Controller
    {
        // GET: Unit
        StockTrackingEntities context = new StockTrackingEntities();
        public ActionResult Index()
        {

            return View(context.Unit.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Unit unit)
        {

            context.Unit.Add(unit);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetUnit(int id)
        {
            var data = context.Unit.Find(id);
            MyUnit unit = new MyUnit();
            unit.UnitId = data.UnitId;
            unit.Unit1 = data.Unit1;
            unit.Description = data.Description;
            return View("GetUnit", unit);
        }
        public ActionResult Update(Unit unit)
        {
            var data = context.Unit.Find(unit.UnitId);
            data.UnitId = unit.UnitId;
            data.Unit1 = unit.Unit1;
            data.Description = unit.Description;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            var data = context.Unit.Find(id);
            context.Unit.Remove(data);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}