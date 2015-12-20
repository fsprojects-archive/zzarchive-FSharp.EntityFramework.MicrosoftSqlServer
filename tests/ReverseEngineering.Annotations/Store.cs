using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Store", Schema = "Sales")]
    public partial class Store
    {
        public Store()
        {
            Customer = new HashSet<Customer>();
        }

        [Key]
        public int BusinessEntityID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public int? SalesPersonID { get; set; }

        [InverseProperty("Store")]
        public virtual ICollection<Customer> Customer { get; set; }
        [ForeignKey("BusinessEntityID")]
        [InverseProperty("Store")]
        public virtual BusinessEntity BusinessEntity { get; set; }
        [ForeignKey("SalesPersonID")]
        [InverseProperty("Store")]
        public virtual SalesPerson SalesPerson { get; set; }
    }
}
