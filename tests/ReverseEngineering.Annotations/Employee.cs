using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Employee", Schema = "HumanResources")]
    public partial class Employee
    {
        public Employee()
        {
            EmployeeDepartmentHistory = new HashSet<EmployeeDepartmentHistory>();
            EmployeePayHistory = new HashSet<EmployeePayHistory>();
            JobCandidate = new HashSet<JobCandidate>();
            PurchaseOrderHeader = new HashSet<PurchaseOrderHeader>();
        }

        [Key]
        public int BusinessEntityID { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(1)]
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string JobTitle { get; set; }
        [Required]
        [MaxLength(256)]
        public string LoginID { get; set; }
        [Required]
        [MaxLength(1)]
        public string MaritalStatus { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(15)]
        public string NationalIDNumber { get; set; }
        public short? OrganizationLevel { get; set; }
        public Guid rowguid { get; set; }
        public short SickLeaveHours { get; set; }
        public short VacationHours { get; set; }

        [InverseProperty("BusinessEntity")]
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<EmployeePayHistory> EmployeePayHistory { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual ICollection<JobCandidate> JobCandidate { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual SalesPerson SalesPerson { get; set; }
        [ForeignKey("BusinessEntityID")]
        [InverseProperty("Employee")]
        public virtual Person BusinessEntity { get; set; }
    }
}
