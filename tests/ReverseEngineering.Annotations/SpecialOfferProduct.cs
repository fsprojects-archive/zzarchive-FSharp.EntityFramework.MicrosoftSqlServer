using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SpecialOfferProduct", Schema = "Sales")]
    public partial class SpecialOfferProduct
    {
        public SpecialOfferProduct()
        {
            SalesOrderDetail = new HashSet<SalesOrderDetail>();
        }

        public int SpecialOfferID { get; set; }
        public int ProductID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [InverseProperty("SpecialOfferProduct")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("SpecialOfferProduct")]
        public virtual Product Product { get; set; }
        [ForeignKey("SpecialOfferID")]
        [InverseProperty("SpecialOfferProduct")]
        public virtual SpecialOffer SpecialOffer { get; set; }
    }
}
