using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Location", Schema = "Production")]
    public partial class Location
    {
        public Location()
        {
            ProductInventory = new HashSet<ProductInventory>();
            WorkOrderRouting = new HashSet<WorkOrderRouting>();
        }

        public short LocationID { get; set; }
        public decimal Availability { get; set; }
        public decimal CostRate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<WorkOrderRouting> WorkOrderRouting { get; set; }
    }
}
