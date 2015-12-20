using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductModelIllustration", Schema = "Production")]
    public partial class ProductModelIllustration
    {
        public int ProductModelID { get; set; }
        public int IllustrationID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("IllustrationID")]
        [InverseProperty("ProductModelIllustration")]
        public virtual Illustration Illustration { get; set; }
        [ForeignKey("ProductModelID")]
        [InverseProperty("ProductModelIllustration")]
        public virtual ProductModel ProductModel { get; set; }
    }
}
