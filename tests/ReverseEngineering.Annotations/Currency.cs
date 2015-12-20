using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Currency", Schema = "Sales")]
    public partial class Currency
    {
        public Currency()
        {
            CountryRegionCurrency = new HashSet<CountryRegionCurrency>();
            CurrencyRate = new HashSet<CurrencyRate>();
            CurrencyRateNavigation = new HashSet<CurrencyRate>();
        }

        [MaxLength(3)]
        [Key]
        public string CurrencyCode { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("CurrencyCodeNavigation")]
        public virtual ICollection<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        [InverseProperty("FromCurrencyCodeNavigation")]
        public virtual ICollection<CurrencyRate> CurrencyRate { get; set; }
        [InverseProperty("ToCurrencyCodeNavigation")]
        public virtual ICollection<CurrencyRate> CurrencyRateNavigation { get; set; }
    }
}
