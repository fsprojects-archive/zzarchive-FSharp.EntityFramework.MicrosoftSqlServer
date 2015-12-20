using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("EmailAddress", Schema = "Person")]
    public partial class EmailAddressTable
    {
        public int BusinessEntityID { get; set; }
        public int EmailAddressID { get; set; }
        [MaxLength(50)]
        public string EmailAddress{ get; set; }
        [Column("EmailAddress")]
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("EmailAddress")]
        public virtual Person BusinessEntity { get; set; }
    }
}
