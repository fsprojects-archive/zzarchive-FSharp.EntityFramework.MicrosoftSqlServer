using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesOrderHeaderSalesReason", Schema = "Sales")]
    public partial class SalesOrderHeaderSalesReason
    {
        public int SalesOrderID { get; set; }
        public int SalesReasonID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("SalesOrderID")]
        [InverseProperty("SalesOrderHeaderSalesReason")]
        public virtual SalesOrderHeader SalesOrder { get; set; }
        [ForeignKey("SalesReasonID")]
        [InverseProperty("SalesOrderHeaderSalesReason")]
        public virtual SalesReason SalesReason { get; set; }
    }
}
