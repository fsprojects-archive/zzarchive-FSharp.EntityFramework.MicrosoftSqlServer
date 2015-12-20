using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductModel", Schema = "Production")]
    public partial class ProductModel
    {
        public ProductModel()
        {
            Product = new HashSet<Product>();
            ProductModelIllustration = new HashSet<ProductModelIllustration>();
            ProductModelProductDescriptionCulture = new HashSet<ProductModelProductDescriptionCulture>();
        }

        public int ProductModelID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }

        [InverseProperty("ProductModel")]
        public virtual ICollection<Product> Product { get; set; }
        [InverseProperty("ProductModel")]
        public virtual ICollection<ProductModelIllustration> ProductModelIllustration { get; set; }
        [InverseProperty("ProductModel")]
        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
    }
}
