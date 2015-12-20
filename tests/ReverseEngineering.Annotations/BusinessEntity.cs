using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("BusinessEntity", Schema = "Person")]
    public partial class BusinessEntity
    {
        public BusinessEntity()
        {
            BusinessEntityAddress = new HashSet<BusinessEntityAddress>();
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
        }

        public int BusinessEntityID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [InverseProperty("BusinessEntity")]
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual Person Person { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual Store Store { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual Vendor Vendor { get; set; }
    }
}
