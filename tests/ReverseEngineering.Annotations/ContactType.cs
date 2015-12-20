using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ContactType", Schema = "Person")]
    public partial class ContactType
    {
        public ContactType()
        {
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
        }

        public int ContactTypeID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ContactType")]
        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }
    }
}
