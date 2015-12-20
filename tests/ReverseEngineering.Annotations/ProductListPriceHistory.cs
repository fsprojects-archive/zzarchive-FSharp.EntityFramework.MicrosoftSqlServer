using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductListPriceHistory", Schema = "Production")]
    public partial class ProductListPriceHistory
    {
        public int ProductID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal ListPrice { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductListPriceHistory")]
        public virtual Product Product { get; set; }
    }
}
