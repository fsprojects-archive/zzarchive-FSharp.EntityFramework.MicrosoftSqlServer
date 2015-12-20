using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ScrapReason", Schema = "Production")]
    public partial class ScrapReason
    {
        public ScrapReason()
        {
            WorkOrder = new HashSet<WorkOrder>();
        }

        public short ScrapReasonID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ScrapReason")]
        public virtual ICollection<WorkOrder> WorkOrder { get; set; }
    }
}
