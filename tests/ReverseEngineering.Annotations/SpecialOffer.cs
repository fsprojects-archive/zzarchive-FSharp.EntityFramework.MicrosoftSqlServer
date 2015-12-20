using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SpecialOffer", Schema = "Sales")]
    public partial class SpecialOffer
    {
        public SpecialOffer()
        {
            SpecialOfferProduct = new HashSet<SpecialOfferProduct>();
        }

        public int SpecialOfferID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Category { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public decimal DiscountPct { get; set; }
        public DateTime EndDate { get; set; }
        public int? MaxQty { get; set; }
        public int MinQty { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid rowguid { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [InverseProperty("SpecialOffer")]
        public virtual ICollection<SpecialOfferProduct> SpecialOfferProduct { get; set; }
    }
}
