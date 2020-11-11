using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracking.MyModel
{
    public class MyUnit
    {
        public MyUnit()
        {
            this.Product = new HashSet<MyProduct>();
        }

        public int UnitId { get; set; }
        public string Unit1 { get; set; }
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyProduct> Product { get; set; }
    }
}