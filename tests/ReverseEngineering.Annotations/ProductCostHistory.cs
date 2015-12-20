using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductCostHistory", Schema = "Production")]
    public partial class ProductCostHistory
    {
        public int ProductID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal StandardCost { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductCostHistory")]
        public virtual Product Product { get; set; }
    }
}
