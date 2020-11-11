using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTracking.MyModel
{
    public class MyProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MyProduct()
        {
            this.Basket = new HashSet<Basket>();
            this.Sales = new HashSet<Sales>();
            this.BrandList = new List<SelectListItem>();
            BrandList.Insert(0, new SelectListItem { Text = "Önce kategori seçilmelidir.", Value = "" });
        }

        public int ProductId { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]

        public int BrandId { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Display(Name = "Barkod No")]

        public string BarcodeNo { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]

        public string ProductName { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Display(Name = "Alış Fiyat")]

        public decimal? PurchasePrice { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Display(Name = "Satış Fiyat")]

        public decimal? SellingPrice { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Range(0, 100, ErrorMessage = "0-100 arasında bir sayı girilmelidir.")]
        [Display(Name = "K.D.V")]

        public int? Vat { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]

        public int UnitId { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bu alan boş geçilemez")]

        public System.DateTime Date { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Display(Name = "Miktar")]

        public Nullable<decimal> Quantity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basket> Basket { get; set; }
        public virtual MyBrand Brand { get; set; }
        public virtual MyCategory Category { get; set; }
        public virtual MyUnit Unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales> Sales { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> BrandList { get; set; }
        public List<SelectListItem> UnitList { get; set; }
    }
}