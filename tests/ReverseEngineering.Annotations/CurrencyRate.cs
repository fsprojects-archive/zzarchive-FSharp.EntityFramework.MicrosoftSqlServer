using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("CurrencyRate", Schema = "Sales")]
    public partial class CurrencyRate
    {
        public CurrencyRate()
        {
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
        }

        public int CurrencyRateID { get; set; }
        public decimal AverageRate { get; set; }
        public DateTime CurrencyRateDate { get; set; }
        public decimal EndOfDayRate { get; set; }
        [Required]
        [MaxLength(3)]
        public string FromCurrencyCode { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(3)]
        public string ToCurrencyCode { get; set; }

        [InverseProperty("CurrencyRate")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
        [ForeignKey("FromCurrencyCode")]
        [InverseProperty("CurrencyRate")]
        public virtual Currency FromCurrencyCodeNavigation { get; set; }
        [ForeignKey("ToCurrencyCode")]
        [InverseProperty("CurrencyRateNavigation")]
        public virtual Currency ToCurrencyCodeNavigation { get; set; }
    }
}
