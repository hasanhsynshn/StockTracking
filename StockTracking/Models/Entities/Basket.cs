//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockTracking.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Basket
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Quantity { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime Hour { get; set; }
        public int UserId { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
