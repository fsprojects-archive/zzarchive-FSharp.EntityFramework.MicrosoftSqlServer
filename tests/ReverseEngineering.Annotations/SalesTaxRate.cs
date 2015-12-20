using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesTaxRate", Schema = "Sales")]
    public partial class SalesTaxRate
    {
        public int SalesTaxRateID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public int StateProvinceID { get; set; }
        public decimal TaxRate { get; set; }
        public byte TaxType { get; set; }

        [ForeignKey("StateProvinceID")]
        [InverseProperty("SalesTaxRate")]
        public virtual StateProvince StateProvince { get; set; }
    }
}
