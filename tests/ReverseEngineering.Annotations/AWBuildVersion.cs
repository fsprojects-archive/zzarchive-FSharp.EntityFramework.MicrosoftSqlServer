using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    public partial class AWBuildVersion
    {
        [Key]
        public byte SystemInformationID { get; set; }
        [Required]
        [MaxLength(25)]
        [Column("Database Version")]
        public string Database_Version { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime VersionDate { get; set; }
    }
}
