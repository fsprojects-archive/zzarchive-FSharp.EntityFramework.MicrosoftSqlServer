using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("PurchaseOrderHeader", Schema = "Purchasing")]
    public partial class PurchaseOrderHeader
    {
        public PurchaseOrderHeader()
        {
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
        }

        [Key]
        public int PurchaseOrderID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Freight { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public byte RevisionNumber { get; set; }
        public DateTime? ShipDate { get; set; }
        public int ShipMethodID { get; set; }
        public byte Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalDue { get; set; }
        public int VendorID { get; set; }

        [InverseProperty("PurchaseOrder")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("PurchaseOrderHeader")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ShipMethodID")]
        [InverseProperty("PurchaseOrderHeader")]
        public virtual ShipMethod ShipMethod { get; set; }
        [ForeignKey("VendorID")]
        [InverseProperty("PurchaseOrderHeader")]
        public virtual Vendor Vendor { get; set; }
    }
}
