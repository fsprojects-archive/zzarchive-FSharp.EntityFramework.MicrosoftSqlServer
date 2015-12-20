using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("BusinessEntityContact", Schema = "Person")]
    public partial class BusinessEntityContact
    {
        public int BusinessEntityID { get; set; }
        public int PersonID { get; set; }
        public int ContactTypeID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("BusinessEntityContact")]
        public virtual BusinessEntity BusinessEntity { get; set; }
        [ForeignKey("ContactTypeID")]
        [InverseProperty("BusinessEntityContact")]
        public virtual ContactType ContactType { get; set; }
        [ForeignKey("PersonID")]
        [InverseProperty("BusinessEntityContact")]
        public virtual Person Person { get; set; }
    }
}
