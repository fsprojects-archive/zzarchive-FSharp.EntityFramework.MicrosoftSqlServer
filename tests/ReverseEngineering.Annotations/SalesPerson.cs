using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesPerson", Schema = "Sales")]
    public partial class SalesPerson
    {
        public SalesPerson()
        {
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
            SalesPersonQuotaHistory = new HashSet<SalesPersonQuotaHistory>();
            SalesTerritoryHistory = new HashSet<SalesTerritoryHistory>();
            Store = new HashSet<Store>();
        }

        [Key]
        public int BusinessEntityID { get; set; }
        public decimal Bonus { get; set; }
        public decimal CommissionPct { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal? SalesQuota { get; set; }
        public decimal SalesYTD { get; set; }
        public int? TerritoryID { get; set; }

        [InverseProperty("SalesPerson")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<SalesPersonQuotaHistory> SalesPersonQuotaHistory { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<SalesTerritoryHistory> SalesTerritoryHistory { get; set; }
        [InverseProperty("SalesPerson")]
        public virtual ICollection<Store> Store { get; set; }
        [ForeignKey("BusinessEntityID")]
        [InverseProperty("SalesPerson")]
        public virtual Employee BusinessEntity { get; set; }
        [ForeignKey("TerritoryID")]
        [InverseProperty("SalesPerson")]
        public virtual SalesTerritory Territory { get; set; }
    }
}
