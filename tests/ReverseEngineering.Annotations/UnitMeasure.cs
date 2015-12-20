using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("UnitMeasure", Schema = "Production")]
    public partial class UnitMeasure
    {
        public UnitMeasure()
        {
            BillOfMaterials = new HashSet<BillOfMaterials>();
            Product = new HashSet<Product>();
            ProductNavigation = new HashSet<Product>();
            ProductVendor = new HashSet<ProductVendor>();
        }

        [MaxLength(3)]
        [Key]
        public string UnitMeasureCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("UnitMeasureCodeNavigation")]
        public virtual ICollection<BillOfMaterials> BillOfMaterials { get; set; }
        [InverseProperty("SizeUnitMeasureCodeNavigation")]
        public virtual ICollection<Product> Product { get; set; }
        [InverseProperty("WeightUnitMeasureCodeNavigation")]
        public virtual ICollection<Product> ProductNavigation { get; set; }
        [InverseProperty("UnitMeasureCodeNavigation")]
        public virtual ICollection<ProductVendor> ProductVendor { get; set; }
    }
}
