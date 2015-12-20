using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("TransactionHistory", Schema = "Production")]
    public partial class TransactionHistory
    {
        [Key]
        public int TransactionID { get; set; }
        public decimal ActualCost { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int ReferenceOrderID { get; set; }
        public int ReferenceOrderLineID { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required]
        [MaxLength(1)]
        public string TransactionType { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("TransactionHistory")]
        public virtual Product Product { get; set; }
    }
}
