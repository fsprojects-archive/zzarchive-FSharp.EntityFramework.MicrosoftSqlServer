using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("CountryRegion", Schema = "Person")]
    public partial class CountryRegion
    {
        public CountryRegion()
        {
            CountryRegionCurrency = new HashSet<CountryRegionCurrency>();
            SalesTerritory = new HashSet<SalesTerritory>();
            StateProvince = new HashSet<StateProvince>();
        }

        [MaxLength(3)]
        [Key]
        public string CountryRegionCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("CountryRegionCodeNavigation")]
        public virtual ICollection<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        [InverseProperty("CountryRegionCodeNavigation")]
        public virtual ICollection<SalesTerritory> SalesTerritory { get; set; }
        [InverseProperty("CountryRegionCodeNavigation")]
        public virtual ICollection<StateProvince> StateProvince { get; set; }
    }
}
