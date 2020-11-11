using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        StockTrackingEntities c = new StockTrackingEntities();
        public ActionResult Index()
        {
            var model = c.Sales.ToList();
            return View(model);
        }
        public ActionResult Sale(int id)
        {
            var model = c.Basket.FirstOrDefault(x => x.BasketId == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Sale2(Sales sale)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var model = c.Basket.FirstOrDefault(x => x.BasketId == sale.BasketId);
                    var product = c.Product.FirstOrDefault(x => x.ProductId == model.ProductId);
                    product.Quantity = product.Quantity - model.Quantity;
                    var sales = new Sales
                    {
                        Id=model.User.Id,
                        ProductId=model.ProductId,
                        BasketId=model.BasketId,
                        BarcodeNo=model.Product.BarcodeNo,
                        UnitPrice=model.PurchasePrice,
                        Quantity=model.Quantity,
                        TotalPrice=model.TotalPrice,
                        Vat=model.Product.Vat,
                        UnitId=model.Product.UnitId,
                        Date=DateTime.Now,
                        Hour=DateTime.Now

                       
                    };
                    c.Basket.Remove(model);
                    c.Sales.Add(sales);
                    c.SaveChanges();
                    ViewBag.Process = "Satın Alma İşlemi Gerçekleşmiştir.";
                }

            }
            catch (Exception)
            {

                ViewBag.Process = "Satın Alma İşlemi Başarısız.";

            }
           
                return View("Process");
           

        }
    }
}