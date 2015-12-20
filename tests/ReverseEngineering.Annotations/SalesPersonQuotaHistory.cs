using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesPersonQuotaHistory", Schema = "Sales")]
    public partial class SalesPersonQuotaHistory
    {
        public int BusinessEntityID { get; set; }
        public DateTime QuotaDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public decimal SalesQuota { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("SalesPersonQuotaHistory")]
        public virtual SalesPerson BusinessEntity { get; set; }
    }
}
