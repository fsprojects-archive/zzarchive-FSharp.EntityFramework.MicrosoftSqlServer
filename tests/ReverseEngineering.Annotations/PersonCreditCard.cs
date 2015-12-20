using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("PersonCreditCard", Schema = "Sales")]
    public partial class PersonCreditCard
    {
        public int BusinessEntityID { get; set; }
        public int CreditCardID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("PersonCreditCard")]
        public virtual Person BusinessEntity { get; set; }
        [ForeignKey("CreditCardID")]
        [InverseProperty("PersonCreditCard")]
        public virtual CreditCard CreditCard { get; set; }
    }
}
