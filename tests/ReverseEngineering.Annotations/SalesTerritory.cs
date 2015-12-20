using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesTerritory", Schema = "Sales")]
    public partial class SalesTerritory
    {
        public SalesTerritory()
        {
            Customer = new HashSet<Customer>();
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
            SalesPerson = new HashSet<SalesPerson>();
            SalesTerritoryHistory = new HashSet<SalesTerritoryHistory>();
            StateProvince = new HashSet<StateProvince>();
        }

        [Key]
        public int TerritoryID { get; set; }
        public decimal CostLastYear { get; set; }
        public decimal CostYTD { get; set; }
        [Required]
        [MaxLength(3)]
        public string CountryRegionCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Group { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal SalesYTD { get; set; }

        [InverseProperty("Territory")]
        public virtual ICollection<Customer> Customer { get; set; }
        [InverseProperty("Territory")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
        [InverseProperty("Territory")]
        public virtual ICollection<SalesPerson> SalesPerson { get; set; }
        [InverseProperty("Territory")]
        public virtual ICollection<SalesTerritoryHistory> SalesTerritoryHistory { get; set; }
        [InverseProperty("Territory")]
        public virtual ICollection<StateProvince> StateProvince { get; set; }
        [ForeignKey("CountryRegionCode")]
        [InverseProperty("SalesTerritory")]
        public virtual CountryRegion CountryRegionCodeNavigation { get; set; }
    }
}
