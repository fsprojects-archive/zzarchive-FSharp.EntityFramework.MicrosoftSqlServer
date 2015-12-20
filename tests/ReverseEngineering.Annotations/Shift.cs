using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Shift", Schema = "HumanResources")]
    public partial class Shift
    {
        public Shift()
        {
            EmployeeDepartmentHistory = new HashSet<EmployeeDepartmentHistory>();
        }

        public byte ShiftID { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ModifiedDate { get; set; }
        public TimeSpan StartTime { get; set; }

        [InverseProperty("Shift")]
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
    }
}
