using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    public partial class DatabaseLog
    {
        public int DatabaseLogID { get; set; }
        public DateTime PostTime { get; set; }
        [Required]
        public string TSQL { get; set; }
    }
}
