using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductInventory", Schema = "Production")]
    public partial class ProductInventory
    {
        public int ProductID { get; set; }
        public short LocationID { get; set; }
        public byte Bin { get; set; }
        public DateTime ModifiedDate { get; set; }
        public short Quantity { get; set; }
        public Guid rowguid { get; set; }
        [Required]
        [MaxLength(10)]
        public string Shelf { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("ProductInventory")]
        public virtual Location Location { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductInventory")]
        public virtual Product Product { get; set; }
    }
}
