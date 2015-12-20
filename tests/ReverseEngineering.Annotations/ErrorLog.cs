using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    public partial class ErrorLog
    {
        public int ErrorLogID { get; set; }
        public int? ErrorLine { get; set; }
        [Required]
        public string ErrorMessage { get; set; }
        public int ErrorNumber { get; set; }
        [MaxLength(126)]
        public string ErrorProcedure { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorState { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}
