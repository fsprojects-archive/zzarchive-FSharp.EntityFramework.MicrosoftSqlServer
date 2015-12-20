using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("EmployeePayHistory", Schema = "HumanResources")]
    public partial class EmployeePayHistory
    {
        public int BusinessEntityID { get; set; }
        public DateTime RateChangeDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte PayFrequency { get; set; }
        public decimal Rate { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("EmployeePayHistory")]
        public virtual Employee BusinessEntity { get; set; }
    }
}
