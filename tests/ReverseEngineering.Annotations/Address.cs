using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Address", Schema = "Person")]
    public partial class Address
    {
        public Address()
        {
            BusinessEntityAddress = new HashSet<BusinessEntityAddress>();
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
            SalesOrderHeaderNavigation = new HashSet<SalesOrderHeader>();
        }

        public int AddressID { get; set; }
        [Required]
        [MaxLength(60)]
        public string AddressLine1 { get; set; }
        [MaxLength(60)]
        public string AddressLine2 { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(15)]
        public string PostalCode { get; set; }
        public Guid rowguid { get; set; }
        public int StateProvinceID { get; set; }

        [InverseProperty("Address")]
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        [InverseProperty("BillToAddress")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
        [InverseProperty("ShipToAddress")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderNavigation { get; set; }
        [ForeignKey("StateProvinceID")]
        [InverseProperty("Address")]
        public virtual StateProvince StateProvince { get; set; }
    }
}
