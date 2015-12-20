using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductSubcategory", Schema = "Production")]
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Product = new HashSet<Product>();
        }

        public int ProductSubcategoryID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProductCategoryID { get; set; }
        public Guid rowguid { get; set; }

        [InverseProperty("ProductSubcategory")]
        public virtual ICollection<Product> Product { get; set; }
        [ForeignKey("ProductCategoryID")]
        [InverseProperty("ProductSubcategory")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
