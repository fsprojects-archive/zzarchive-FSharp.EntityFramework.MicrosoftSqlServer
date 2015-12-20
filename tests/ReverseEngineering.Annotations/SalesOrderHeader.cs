using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultReverseEngineering
{
    [Table("SalesOrderHeader", Schema = "Sales")]
    public partial class SalesOrderHeader
    {
        public SalesOrderHeader()
        {
            SalesOrderDetail = new HashSet<SalesOrderDetail>();
            SalesOrderHeaderSalesReason = new HashSet<SalesOrderHeaderSalesReason>();
        }

        [Key]
        public int SalesOrderID { get; set; }
        public int BillToAddressID { get; set; }
        [MaxLength(128)]
        public string Comment { get; set; }
        [MaxLength(15)]
        public string CreditCardApprovalCode { get; set; }
        public int? CreditCardID { get; set; }
        public int? CurrencyRateID { get; set; }
        public int CustomerID { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Freight { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public byte RevisionNumber { get; set; }
        public Guid rowguid { get; set; }
        [Required]
        [MaxLength(25)]
        public string SalesOrderNumber { get; set; }
        public int? SalesPersonID { get; set; }
        public DateTime? ShipDate { get; set; }
        public int ShipMethodID { get; set; }
        public int ShipToAddressID { get; set; }
        public byte Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public int? TerritoryID { get; set; }
        public decimal TotalDue { get; set; }

        [InverseProperty("SalesOrder")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }
        [InverseProperty("SalesOrder")]
        public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
        [ForeignKey("BillToAddressID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual Address BillToAddress { get; set; }
        [ForeignKey("CreditCardID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual CreditCard CreditCard { get; set; }
        [ForeignKey("CurrencyRateID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual CurrencyRate CurrencyRate { get; set; }
        [ForeignKey("CustomerID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("SalesPersonID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual SalesPerson SalesPerson { get; set; }
        [ForeignKey("ShipMethodID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual ShipMethod ShipMethod { get; set; }
        [ForeignKey("ShipToAddressID")]
        [InverseProperty("SalesOrderHeaderNavigation")]
        public virtual Address ShipToAddress { get; set; }
        [ForeignKey("TerritoryID")]
        [InverseProperty("SalesOrderHeader")]
        public virtual SalesTerritory Territory { get; set; }
    }
}
