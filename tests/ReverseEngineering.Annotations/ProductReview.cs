using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("ProductReview", Schema = "Production")]
    public partial class ProductReview
    {
        public int ProductReviewID { get; set; }
        [MaxLength(3850)]
        public string Comments { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductReview")]
        public virtual Product Product { get; set; }
    }
}
