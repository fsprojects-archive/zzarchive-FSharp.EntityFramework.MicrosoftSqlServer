using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductVendor", Schema = "Purchasing")]
    public partial class ProductVendor
    {
        public int ProductID { get; set; }
        public int BusinessEntityID { get; set; }
        public int AverageLeadTime { get; set; }
        public decimal? LastReceiptCost { get; set; }
        public DateTime? LastReceiptDate { get; set; }
        public int MaxOrderQty { get; set; }
        public int MinOrderQty { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? OnOrderQty { get; set; }
        public decimal StandardPrice { get; set; }
        [Required]
        [MaxLength(3)]
        public string UnitMeasureCode { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("ProductVendor")]
        public virtual Vendor BusinessEntity { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductVendor")]
        public virtual Product Product { get; set; }
        [ForeignKey("UnitMeasureCode")]
        [InverseProperty("ProductVendor")]
        public virtual UnitMeasure UnitMeasureCodeNavigation { get; set; }
    }
}
