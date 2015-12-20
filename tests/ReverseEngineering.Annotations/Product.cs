using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("Product", Schema = "Production")]
    public partial class Product
    {
        public Product()
        {
            BillOfMaterials = new HashSet<BillOfMaterials>();
            BillOfMaterialsNavigation = new HashSet<BillOfMaterials>();
            ProductCostHistory = new HashSet<ProductCostHistory>();
            ProductInventory = new HashSet<ProductInventory>();
            ProductListPriceHistory = new HashSet<ProductListPriceHistory>();
            ProductProductPhoto = new HashSet<ProductProductPhoto>();
            ProductReview = new HashSet<ProductReview>();
            ProductVendor = new HashSet<ProductVendor>();
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
            SpecialOfferProduct = new HashSet<SpecialOfferProduct>();
            TransactionHistory = new HashSet<TransactionHistory>();
            WorkOrder = new HashSet<WorkOrder>();
        }

        public int ProductID { get; set; }
        [MaxLength(2)]
        public string Class { get; set; }
        [MaxLength(15)]
        public string Color { get; set; }
        public int DaysToManufacture { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public decimal ListPrice { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(2)]
        public string ProductLine { get; set; }
        public int? ProductModelID { get; set; }
        [Required]
        [MaxLength(25)]
        public string ProductNumber { get; set; }
        public int? ProductSubcategoryID { get; set; }
        public short ReorderPoint { get; set; }
        public Guid rowguid { get; set; }
        public short SafetyStockLevel { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime SellStartDate { get; set; }
        [MaxLength(5)]
        public string Size { get; set; }
        [MaxLength(3)]
        public string SizeUnitMeasureCode { get; set; }
        public decimal StandardCost { get; set; }
        [MaxLength(2)]
        public string Style { get; set; }
        public decimal? Weight { get; set; }
        [MaxLength(3)]
        public string WeightUnitMeasureCode { get; set; }

        [InverseProperty("Component")]
        public virtual ICollection<BillOfMaterials> BillOfMaterials { get; set; }
        [InverseProperty("ProductAssembly")]
        public virtual ICollection<BillOfMaterials> BillOfMaterialsNavigation { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductCostHistory> ProductCostHistory { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductListPriceHistory> ProductListPriceHistory { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductProductPhoto> ProductProductPhoto { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductReview> ProductReview { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductVendor> ProductVendor { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<SpecialOfferProduct> SpecialOfferProduct { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TransactionHistory> TransactionHistory { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<WorkOrder> WorkOrder { get; set; }
        [ForeignKey("ProductModelID")]
        [InverseProperty("Product")]
        public virtual ProductModel ProductModel { get; set; }
        [ForeignKey("ProductSubcategoryID")]
        [InverseProperty("Product")]
        public virtual ProductSubcategory ProductSubcategory { get; set; }
        [ForeignKey("SizeUnitMeasureCode")]
        [InverseProperty("Product")]
        public virtual UnitMeasure SizeUnitMeasureCodeNavigation { get; set; }
        [ForeignKey("WeightUnitMeasureCode")]
        [InverseProperty("ProductNavigation")]
        public virtual UnitMeasure WeightUnitMeasureCodeNavigation { get; set; }
    }
}
