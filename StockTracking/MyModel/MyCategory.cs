using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracking.MyModel
{
    public class MyCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MyCategory()
        {
            this.Brand = new HashSet<MyBrand>();
            this.Product = new HashSet<MyProduct>();
        }

        public int CategoryId { get; set; }
        public string Category1 { get; set; }
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyBrand> Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyProduct> Product { get; set; }
    }
}