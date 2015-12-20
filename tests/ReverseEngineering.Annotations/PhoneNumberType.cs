using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("PhoneNumberType", Schema = "Person")]
    public partial class PhoneNumberType
    {
        public int PhoneNumberTypeID { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
