using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("WorkOrderRouting", Schema = "Production")]
    public partial class WorkOrderRouting
    {
        public int WorkOrderID { get; set; }
        public int ProductID { get; set; }
        public short OperationSequence { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? ActualResourceHrs { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public short LocationID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal PlannedCost { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public DateTime ScheduledStartDate { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("WorkOrderRouting")]
        public virtual Location Location { get; set; }
        [ForeignKey("WorkOrderID")]
        [InverseProperty("WorkOrderRouting")]
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
