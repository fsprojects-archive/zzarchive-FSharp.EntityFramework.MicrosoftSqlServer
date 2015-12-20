using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ShipMethod", Schema = "Purchasing")]
    public partial class ShipMethod
    {
        public ShipMethod()
        {
            PurchaseOrderHeader = new HashSet<PurchaseOrderHeader>();
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
        }

        public int ShipMethodID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }

        [InverseProperty("ShipMethod")]
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }
        [InverseProperty("ShipMethod")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
    }
}
