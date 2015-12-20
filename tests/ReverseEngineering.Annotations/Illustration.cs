using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Illustration", Schema = "Production")]
    public partial class Illustration
    {
        public Illustration()
        {
            ProductModelIllustration = new HashSet<ProductModelIllustration>();
        }

        public int IllustrationID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Illustration")]
        public virtual ICollection<ProductModelIllustration> ProductModelIllustration { get; set; }
    }
}
