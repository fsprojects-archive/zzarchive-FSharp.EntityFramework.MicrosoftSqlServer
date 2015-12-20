using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesTerritoryHistory", Schema = "Sales")]
    public partial class SalesTerritoryHistory
    {
        public int BusinessEntityID { get; set; }
        public DateTime StartDate { get; set; }
        public int TerritoryID { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("SalesTerritoryHistory")]
        public virtual SalesPerson BusinessEntity { get; set; }
        [ForeignKey("TerritoryID")]
        [InverseProperty("SalesTerritoryHistory")]
        public virtual SalesTerritory Territory { get; set; }
    }
}
