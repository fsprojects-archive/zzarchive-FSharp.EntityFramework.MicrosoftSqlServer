using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Password", Schema = "Person")]
    public partial class Password
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(128)]
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(10)]
        public string PasswordSalt { get; set; }
        public Guid rowguid { get; set; }

        [ForeignKey("BusinessEntityID")]
        [InverseProperty("Password")]
        public virtual Person BusinessEntity { get; set; }
    }
}
