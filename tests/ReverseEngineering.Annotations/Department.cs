using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Department", Schema = "HumanResources")]
    public partial class Department
    {
        public Department()
        {
            EmployeeDepartmentHistory = new HashSet<EmployeeDepartmentHistory>();
        }

        public short DepartmentID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
    }
}
