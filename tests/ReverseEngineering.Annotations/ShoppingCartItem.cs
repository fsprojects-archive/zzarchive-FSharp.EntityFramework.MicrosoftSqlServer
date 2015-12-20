using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ShoppingCartItem", Schema = "Sales")]
    public partial class ShoppingCartItem
    {
        public int ShoppingCartItemID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        [Required]
        [MaxLength(50)]
        public string ShoppingCartID { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ShoppingCartItem")]
        public virtual Product Product { get; set; }
    }
}
