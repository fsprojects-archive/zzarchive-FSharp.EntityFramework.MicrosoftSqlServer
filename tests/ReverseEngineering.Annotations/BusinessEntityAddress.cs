using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("BusinessEntityAddress", Schema = "Person")]
    public partial class BusinessEntityAddress
    {
        public int BusinessEntityID { get; set; }
        public int AddressID { get; set; }
        public int AddressTypeID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [ForeignKey("AddressID")]
        [InverseProperty("BusinessEntityAddress")]
        public virtual Address Address { get; set; }
        [ForeignKey("AddressTypeID")]
        [InverseProperty("BusinessEntityAddress")]
        public virtual AddressType AddressType { get; set; }
        [ForeignKey("BusinessEntityID")]
        [InverseProperty("BusinessEntityAddress")]
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
