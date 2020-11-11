using StockTracking.Models.Entities;
using StockTracking.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
namespace StockTracking.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        StockTrackingEntities c = new StockTrackingEntities();
        public ActionResult Index(string search)
        {
            var data = c.Product.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.ProductName.Contains(search) || x.BarcodeNo.Contains(search)).ToList();

            }
            return View(data);
        }
        public ActionResult Index2(string search)
        {
            var data = c.Product.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.ProductName.Contains(search) || x.BarcodeNo.Contains(search)).ToList();

            }
            return View(data);
        }

        public ActionResult Add()
        {
            var model = new MyProduct();
            Reload(model);
            return View(model);

        }

        private void Reload(MyProduct model)
        {
            List<Category> categoryList = c.Category.OrderBy(x => x.Category1).ToList();
            model.CategoryList = (from x in categoryList
                                  select new SelectListItem
                                  {
                                      Text = x.Category1,
                                      Value = x.CategoryId.ToString()
                                  }).ToList();

            List<Unit> unitList = c.Unit.OrderBy(x => x.UnitId).ToList();
            model.UnitList = (from x in unitList
                              select new SelectListItem
                              {
                                  Text = x.Unit1,
                                  Value = x.UnitId.ToString()
                              }).ToList();
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                var model = new MyProduct();
                Reload(model);
                return View(model);
            }
            c.Product.Add(product);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult GetBrand(int id)
        {
            var model = new MyProduct();
            List<Brand> brandList = c.Brand.Where(x => x.CategoryId == id).OrderBy(x => x.Brand1).ToList();
            model.BrandList = (from x in brandList
                               select new SelectListItem
                               {
                                   Text = x.Brand1,
                                   Value = x.BrandId.ToString()
                               }).ToList();
            model.BrandList.Insert(0, new SelectListItem { Text = "Seçiniz", Value = "" });
            return Json(model.BrandList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QuantityAdd(int id)
        {
            var model = c.Product.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult QuantityAdd(Product product)
        {
            var model = c.Product.Find(product.ProductId);
            model.Quantity = model.Quantity + product.Quantity;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetUpdate(int id)
        {
            var data = c.Product.Find(id);

            var product = new MyProduct();
            product.ProductId = data.ProductId;
            product.CategoryId = data.CategoryId;
            product.BrandId = data.BrandId;
            product.ProductName = data.ProductName;
            product.BarcodeNo = data.BarcodeNo;
            product.PurchasePrice = data.PurchasePrice;
            product.SellingPrice = data.SellingPrice;
            product.Quantity = data.Quantity;
            product.Vat = data.Vat;
            product.UnitId = data.UnitId;
            product.Date = data.Date;
            product.Description = data.Description;

            Reload(product);

            List<Brand> brandList =
                c.Brand.Where(x => x.CategoryId == data.CategoryId).
                OrderBy(x => x.Brand1).ToList();
            product.BrandList = (from x in brandList
                                 select new SelectListItem
                                 {
                                     Text = x.Brand1,
                                     Value = x.BrandId.ToString()
                                 }).ToList();
            return View(product);

        }
        [HttpPost]
        public ActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
            {
                var data = c.Product.Find(product.ProductId);
                var myproduct = new MyProduct();
                Reload(myproduct);
                List<Brand> brandList =
                    c.Brand.Where(x => x.CategoryId == data.CategoryId).
                    OrderBy(x => x.Brand1).ToList();
                myproduct.BrandList = (from x in brandList
                                       select new SelectListItem
                                       {
                                           Text = x.Brand1,
                                           Value = x.BrandId.ToString()
                                       }).ToList();
                return View(myproduct);
            }
            c.Entry(product).State = System.Data.Entity.EntityState.Modified;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var data = c.Product.FirstOrDefault(x => x.ProductId == id);
            c.Entry(data).State = System.Data.Entity.EntityState.Deleted;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult ExcelExport()
        {
            try
            {
                var list = c.Product.ToList();
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "Ürün Adı";
                worksheet.Cells[1, 3] = "Barkod No";
                worksheet.Cells[1, 4] = "Fiyatı";
                worksheet.Cells[1, 5] = "Miktarı";
                worksheet.Cells[1, 6] = "Tarih";

                int row = 2;
                foreach (var item in list)
                {
                    worksheet.Cells[row, 1] = item.ProductId;
                    worksheet.Cells[row, 2] = item.ProductName;
                    worksheet.Cells[row, 3] = item.BarcodeNo;
                    worksheet.Cells[row, 4] = item.SellingPrice;
                    worksheet.Cells[row, 5] = item.Quantity;
                    worksheet.Cells[row, 6] = item.Date;
                    worksheet.Cells[row, 2].ColumnWidth = 15;
                    worksheet.Cells[row, 4].ColumnWidth = 15;
                    worksheet.Cells[row, 6].ColumnWidth = 15;
                    row++;
                }
                var heading = worksheet.get_Range("A1", "F1");
                heading.Font.Bold = true;
                heading.Font.Size = 13;
                heading.Font.Color = System.Drawing.Color.Red;
                var sum = c.Product.Sum(x => x.SellingPrice).ToString("#,###,###.00");
                var range_sum = worksheet.get_Range("D" + row);
                range_sum.Value2 = "Total" + sum;
                range_sum.Font.Bold = true;
                var range_Fiyat = worksheet.get_Range("D2","D"+row);
                range_Fiyat.NumberFormat = "#,###,###.00";
                var range_Tarih = worksheet.get_Range("F2", "F" + row);
                range_Tarih.NumberFormat = "dd.MM.yyyy";

                workbook.SaveAs("C:\\Games\\deneme.xlsx");
                workbook.Close();
                application.Quit();



                ViewBag.Message = "İşlem Başarılı";

            }
            catch (Exception)
            {
                ViewBag.Message = "İşlem Başarısız!";
                throw;
            }
            return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
        }


    }
}