using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("EmployeeDepartmentHistory", Schema = "HumanResources")]
    public partial class EmployeeDepartmentHistory
    {
        public int BusinessEntityID { get; set; }
        public DateTime StartDate { get; set; }
        public short DepartmentID { get; set; }
        public byte ShiftID { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("EmployeeDepartmentHistory")]
        public virtual Employee BusinessEntity { get; set; }
        [ForeignKey("DepartmentID")]
        [InverseProperty("EmployeeDepartmentHistory")]
        public virtual Department Department { get; set; }
        [ForeignKey("ShiftID")]
        [InverseProperty("EmployeeDepartmentHistory")]
        public virtual Shift Shift { get; set; }
    }
}
