using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("PurchaseOrderDetail", Schema = "Purchasing")]
    public partial class PurchaseOrderDetail
    {
        public int PurchaseOrderID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public DateTime DueDate { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime ModifiedDate { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal RejectedQty { get; set; }
        public decimal StockedQty { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("PurchaseOrderDetail")]
        public virtual Product Product { get; set; }
        [ForeignKey("PurchaseOrderID")]
        [InverseProperty("PurchaseOrderDetail")]
        public virtual PurchaseOrderHeader PurchaseOrder { get; set; }
    }
}
