using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("BillOfMaterials", Schema = "Production")]
    public partial class BillOfMaterials
    {
        public int BillOfMaterialsID { get; set; }
        public short BOMLevel { get; set; }
        public int ComponentID { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal PerAssemblyQty { get; set; }
        public int? ProductAssemblyID { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        [MaxLength(3)]
        public string UnitMeasureCode { get; set; }

        [ForeignKey("ComponentID")]
        [InverseProperty("BillOfMaterials")]
        public virtual Product Component { get; set; }
        [ForeignKey("ProductAssemblyID")]
        [InverseProperty("BillOfMaterialsNavigation")]
        public virtual Product ProductAssembly { get; set; }
        [ForeignKey("UnitMeasureCode")]
        [InverseProperty("BillOfMaterials")]
        public virtual UnitMeasure UnitMeasureCodeNavigation { get; set; }
    }
}
