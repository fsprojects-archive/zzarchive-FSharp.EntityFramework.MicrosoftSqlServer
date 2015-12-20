using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductPhoto", Schema = "Production")]
    public partial class ProductPhoto
    {
        public ProductPhoto()
        {
            ProductProductPhoto = new HashSet<ProductProductPhoto>();
        }

        public int ProductPhotoID { get; set; }
        public byte[] LargePhoto { get; set; }
        [MaxLength(50)]
        public string LargePhotoFileName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        [MaxLength(50)]
        public string ThumbnailPhotoFileName { get; set; }

        [InverseProperty("ProductPhoto")]
        public virtual ICollection<ProductProductPhoto> ProductProductPhoto { get; set; }
    }
}
