using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductModelProductDescriptionCulture", Schema = "Production")]
    public partial class ProductModelProductDescriptionCulture
    {
        public int ProductModelID { get; set; }
        public int ProductDescriptionID { get; set; }
        [MaxLength(6)]
        public string CultureID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductModelProductDescriptionCulture")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ProductDescriptionID")]
        [InverseProperty("ProductModelProductDescriptionCulture")]
        public virtual ProductDescription ProductDescription { get; set; }
        [ForeignKey("ProductModelID")]
        [InverseProperty("ProductModelProductDescriptionCulture")]
        public virtual ProductModel ProductModel { get; set; }
    }
}
