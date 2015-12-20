using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("WorkOrder", Schema = "Production")]
    public partial class WorkOrder
    {
        public WorkOrder()
        {
            WorkOrderRouting = new HashSet<WorkOrderRouting>();
        }

        public int WorkOrderID { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int OrderQty { get; set; }
        public int ProductID { get; set; }
        public short ScrappedQty { get; set; }
        public short? ScrapReasonID { get; set; }
        public DateTime StartDate { get; set; }
        public int StockedQty { get; set; }

        [InverseProperty("WorkOrder")]
        public virtual ICollection<WorkOrderRouting> WorkOrderRouting { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("WorkOrder")]
        public virtual Product Product { get; set; }
        [ForeignKey("ScrapReasonID")]
        [InverseProperty("WorkOrder")]
        public virtual ScrapReason ScrapReason { get; set; }
    }
}
