using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("JobCandidate", Schema = "HumanResources")]
    public partial class JobCandidate
    {
        public int JobCandidateID { get; set; }
        public int? BusinessEntityID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("JobCandidate")]
        public virtual Employee BusinessEntity { get; set; }
    }
}
