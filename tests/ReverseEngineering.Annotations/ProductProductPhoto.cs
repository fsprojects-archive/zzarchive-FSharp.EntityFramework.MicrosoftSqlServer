using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductProductPhoto", Schema = "Production")]
    public partial class ProductProductPhoto
    {
        public int ProductID { get; set; }
        public int ProductPhotoID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductProductPhoto")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductPhotoID")]
        [InverseProperty("ProductProductPhoto")]
        public virtual ProductPhoto ProductPhoto { get; set; }
    }
}
