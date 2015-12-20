using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Customer", Schema = "Sales")]
    public partial class Customer
    {
        public Customer()
        {
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
        }

        public int CustomerID { get; set; }
        [Required]
        [MaxLength(10)]
        public string AccountNumber { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? PersonID { get; set; }
        public Guid rowguid { get; set; }
        public int? StoreID { get; set; }
        public int? TerritoryID { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
        [ForeignKey("PersonID")]
        [InverseProperty("Customer")]
        public virtual Person Person { get; set; }
        [ForeignKey("StoreID")]
        [InverseProperty("Customer")]
        public virtual Store Store { get; set; }
        [ForeignKey("TerritoryID")]
        [InverseProperty("Customer")]
        public virtual SalesTerritory Territory { get; set; }
    }
}
