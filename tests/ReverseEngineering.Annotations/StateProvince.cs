using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("StateProvince", Schema = "Person")]
    public partial class StateProvince
    {
        public StateProvince()
        {
            Address = new HashSet<Address>();
            SalesTaxRate = new HashSet<SalesTaxRate>();
        }

        public int StateProvinceID { get; set; }
        [Required]
        [MaxLength(3)]
        public string CountryRegionCode { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        [Required]
        [MaxLength(3)]
        public string StateProvinceCode { get; set; }
        public int TerritoryID { get; set; }

        [InverseProperty("StateProvince")]
        public virtual ICollection<Address> Address { get; set; }
        [InverseProperty("StateProvince")]
        public virtual ICollection<SalesTaxRate> SalesTaxRate { get; set; }
        [ForeignKey("CountryRegionCode")]
        [InverseProperty("StateProvince")]
        public virtual CountryRegion CountryRegionCodeNavigation { get; set; }
        [ForeignKey("TerritoryID")]
        [InverseProperty("StateProvince")]
        public virtual SalesTerritory Territory { get; set; }
    }
}
