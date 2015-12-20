using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("CreditCard", Schema = "Sales")]
    public partial class CreditCard
    {
        public CreditCard()
        {
            PersonCreditCard = new HashSet<PersonCreditCard>();
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
        }

        public int CreditCardID { get; set; }
        [Required]
        [MaxLength(25)]
        public string CardNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string CardType { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("CreditCard")]
        public virtual ICollection<PersonCreditCard> PersonCreditCard { get; set; }
        [InverseProperty("CreditCard")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
    }
}
