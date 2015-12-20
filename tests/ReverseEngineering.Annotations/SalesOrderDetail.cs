using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesOrderDetail", Schema = "Sales")]
    public partial class SalesOrderDetail
    {
        public int SalesOrderID { get; set; }
        public int SalesOrderDetailID { get; set; }
        [MaxLength(25)]
        public string CarrierTrackingNumber { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime ModifiedDate { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public Guid rowguid { get; set; }
        public int SpecialOfferID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }

        [ForeignKey("SalesOrderID")]
        [InverseProperty("SalesOrderDetail")]
        public virtual SalesOrderHeader SalesOrder { get; set; }
        [ForeignKey("SpecialOfferID,ProductID")]
        [InverseProperty("SalesOrderDetail")]
        public virtual SpecialOfferProduct SpecialOfferProduct { get; set; }
    }
}
