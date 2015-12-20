using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("CountryRegionCurrency", Schema = "Sales")]
    public partial class CountryRegionCurrency
    {
        [MaxLength(3)]
        public string CountryRegionCode { get; set; }
        [MaxLength(3)]
        public string CurrencyCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("CountryRegionCode")]
        [InverseProperty("CountryRegionCurrency")]
        public virtual CountryRegion CountryRegionCodeNavigation { get; set; }
        [ForeignKey("CurrencyCode")]
        [InverseProperty("CountryRegionCurrency")]
        public virtual Currency CurrencyCodeNavigation { get; set; }
    }
}
