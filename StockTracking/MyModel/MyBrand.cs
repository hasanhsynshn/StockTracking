using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracking.MyModel
{
    public class MyBrand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MyBrand()
        {
            this.Product = new HashSet<MyProduct>();
        }

        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Brand1 { get; set; }
        public string Description { get; set; }

        public virtual MyCategory Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyProduct> Product { get; set; }
    }
}