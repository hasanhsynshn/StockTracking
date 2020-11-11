using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.Controllers
{

    public class BasketController : Controller
    {
        StockTrackingEntities c = new StockTrackingEntities();
        // GET: Basket
        public ActionResult Index(decimal? Price)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = c.User.FirstOrDefault(x => x.UserName == userName);
                var model = c.Basket.Where(x => x.UserId == user.Id).ToList();
                var uId = c.Basket.FirstOrDefault(x => x.UserId == user.Id);
                if (model != null)
                {
                    if (uId == null)
                    {
                        ViewBag.Price = "Sepetinizde ürün bulunmuyor";
                    }
                    else if (uId != null)
                    {
                        Price = c.Basket.Where(x => x.UserId == uId.UserId).Sum(x => x.TotalPrice);
                        ViewBag.Price = "Toplam Tutar=" + Price + " TL ";
                    }
                    return View(model);
                }

            }
            return HttpNotFound();
        }


        public ActionResult AddBasket(int id)
        {

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var model = c.User.FirstOrDefault(x => x.UserName == userName);
                var product = c.Product.Find(id);
                var basket = c.Basket.FirstOrDefault
                    (x => x.UserId == model.Id && x.ProductId == id);
                if (model != null)
                {
                    if (basket != null)
                    {
                        basket.Quantity++;
                        basket.TotalPrice = product.SellingPrice * basket.Quantity;
                        c.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    var bas = new Basket
                    {
                        UserId = model.Id,
                        ProductId = product.ProductId,
                        Quantity = 1,
                        PurchasePrice = product.SellingPrice,
                        TotalPrice = product.SellingPrice,
                        Date = DateTime.Now,
                        Hour = DateTime.Now
                    };
                    c.Entry(bas).State = System.Data.Entity.EntityState.Added;
                    c.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            return HttpNotFound();
        }
        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = c.User.FirstOrDefault(x => x.UserName == User.Identity.Name);
                count = c.Basket.Where(x => x.UserId == model.Id).Count();
                ViewBag.Count = count;
                if (count == 0)
                {
                    ViewBag.Count = " ";
                }



                return PartialView();
            }
            return HttpNotFound();
        }
        public ActionResult Arttir(int id)
        {
            var model = c.Basket.Find(id);
            model.Quantity++;
            model.TotalPrice = model.PurchasePrice * model.Quantity;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Azalt(int id)
        {
            var model = c.Basket.Find(id);
            if (model.Quantity == 1)
            {
                c.Basket.Remove(model);
                c.SaveChanges();
            }
            model.Quantity--;
            model.TotalPrice = model.PurchasePrice * model.Quantity;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public void DynamicQuantity(int id, decimal quantity)
        {
            var model = c.Basket.Find(id);
            model.Quantity = quantity;
            model.TotalPrice = model.PurchasePrice * model.Quantity;
            c.SaveChanges();
        }
        public ActionResult Delete(int id)
        {
            var model = c.Basket.Find(id);
            c.Basket.Remove(model);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult AllofDelete()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var model = c.User.FirstOrDefault(x => x.UserName == userName);
                var deleteAll = c.Basket.Where(x => x.UserId == model.Id);
                c.Basket.RemoveRange(deleteAll);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}