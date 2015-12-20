using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesReason", Schema = "Sales")]
    public partial class SalesReason
    {
        public SalesReason()
        {
            SalesOrderHeaderSalesReason = new HashSet<SalesOrderHeaderSalesReason>();
        }

        public int SalesReasonID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("SalesReason")]
        public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
    }
}
