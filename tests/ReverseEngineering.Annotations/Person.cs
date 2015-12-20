using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Person", Schema = "Person")]
    public partial class Person
    {
        public Person()
        {
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
            Customer = new HashSet<Customer>();
            EmailAddress = new HashSet<EmailAddressTable>();
            PersonCreditCard = new HashSet<PersonCreditCard>();
        }

        [Key]
        public int BusinessEntityID { get; set; }
        public int EmailPromotion { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(2)]
        public string PersonType { get; set; }
        public Guid rowguid { get; set; }
        [MaxLength(10)]
        public string Suffix { get; set; }
        [MaxLength(8)]
        public string Title { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Customer> Customer { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<EmailAddressTable> EmailAddress { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual Employee Employee { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual Password Password { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<PersonCreditCard> PersonCreditCard { get; set; }
        [ForeignKey("BusinessEntityID")]
        [InverseProperty("Person")]
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
