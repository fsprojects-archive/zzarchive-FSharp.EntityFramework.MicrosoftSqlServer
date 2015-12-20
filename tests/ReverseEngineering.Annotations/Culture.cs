using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Culture", Schema = "Production")]
    public partial class Culture
    {
        public Culture()
        {
            ProductModelProductDescriptionCulture = new HashSet<ProductModelProductDescriptionCulture>();
        }

        [MaxLength(6)]
        public string CultureID { get; set; }
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Culture")]
        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
    }
}
