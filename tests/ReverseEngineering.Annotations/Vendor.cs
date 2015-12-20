using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Vendor", Schema = "Purchasing")]
    public partial class Vendor
    {
        public Vendor()
        {
            ProductVendor = new HashSet<ProductVendor>();
            PurchaseOrderHeader = new HashSet<PurchaseOrderHeader>();
        }

        [Key]
        public int BusinessEntityID { get; set; }
        public byte CreditRating { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(1024)]
        public string PurchasingWebServiceURL { get; set; }

        [InverseProperty("BusinessEntity")]
        public virtual ICollection<ProductVendor> ProductVendor { get; set; }
        [InverseProperty("Vendor")]
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }
        [ForeignKey("BusinessEntityID")]
        [InverseProperty("Vendor")]
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
