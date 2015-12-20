using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace DefaultReverseEngineering
{
    public partial class AdventureWorks2014Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=.;Database=AdventureWorks2014;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AWBuildVersion>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.VersionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.StateProvinceID).HasName("IX_Address_StateProvinceID");

                entity.HasIndex(e => e.rowguid).HasName("AK_Address_rowguid").IsUnique();

                entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvinceID, e.PostalCode }).HasName("IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_AddressType_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<BillOfMaterials>(entity =>
            {
                entity.HasIndex(e => e.UnitMeasureCode).HasName("IX_BillOfMaterials_UnitMeasureCode");

                entity.HasIndex(e => new { e.ProductAssemblyID, e.ComponentID, e.StartDate }).HasName("AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate").IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.PerAssemblyQty)
                    .HasColumnType("decimal")
                    .HasDefaultValue(1.00m);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.UnitMeasureCode).HasColumnType("nchar");
            });

            modelBuilder.Entity<BusinessEntity>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_BusinessEntity_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<BusinessEntityAddress>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.AddressID, e.AddressTypeID });

                entity.HasIndex(e => e.AddressID).HasName("IX_BusinessEntityAddress_AddressID");

                entity.HasIndex(e => e.AddressTypeID).HasName("IX_BusinessEntityAddress_AddressTypeID");

                entity.HasIndex(e => e.rowguid).HasName("AK_BusinessEntityAddress_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<BusinessEntityContact>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.PersonID, e.ContactTypeID });

                entity.HasIndex(e => e.ContactTypeID).HasName("IX_BusinessEntityContact_ContactTypeID");

                entity.HasIndex(e => e.PersonID).HasName("IX_BusinessEntityContact_PersonID");

                entity.HasIndex(e => e.rowguid).HasName("AK_BusinessEntityContact_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<CountryRegion>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<CountryRegionCurrency>(entity =>
            {
                entity.HasKey(e => new { e.CountryRegionCode, e.CurrencyCode });

                entity.HasIndex(e => e.CurrencyCode).HasName("IX_CountryRegionCurrency_CurrencyCode");

                entity.Property(e => e.CurrencyCode).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasIndex(e => e.CardNumber).HasName("AK_CreditCard_CardNumber").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Culture>(entity =>
            {
                entity.Property(e => e.CultureID).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyCode).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<CurrencyRate>(entity =>
            {
                entity.HasIndex(e => new { e.CurrencyRateDate, e.FromCurrencyCode, e.ToCurrencyCode }).HasName("AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode").IsUnique();

                entity.Property(e => e.AverageRate).HasColumnType("money");

                entity.Property(e => e.CurrencyRateDate).HasColumnType("datetime");

                entity.Property(e => e.EndOfDayRate).HasColumnType("money");

                entity.Property(e => e.FromCurrencyCode).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ToCurrencyCode).HasColumnType("nchar");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.AccountNumber).HasName("AK_Customer_AccountNumber").IsUnique();

                entity.HasIndex(e => e.TerritoryID).HasName("IX_Customer_TerritoryID");

                entity.HasIndex(e => e.rowguid).HasName("AK_Customer_rowguid").IsUnique();

                entity.Property(e => e.AccountNumber)
                    .HasColumnType("varchar")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<DatabaseLog>(entity =>
            {
                entity.Property(e => e.PostTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<EmailAddressTable>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.EmailAddressID });

                entity.HasIndex(e => e.EmailAddress).HasName("IX_EmailAddress_EmailAddress");

                entity.Property(e => e.EmailAddressID).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.LoginID).HasName("AK_Employee_LoginID").IsUnique();

                entity.HasIndex(e => e.NationalIDNumber).HasName("AK_Employee_NationalIDNumber").IsUnique();

                entity.HasIndex(e => e.rowguid).HasName("AK_Employee_rowguid").IsUnique();

                entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Gender).HasColumnType("nchar");

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.MaritalStatus).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.OrganizationLevel).ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.SickLeaveHours).HasDefaultValue(0);

                entity.Property(e => e.VacationHours).HasDefaultValue(0);
            });

            modelBuilder.Entity<EmployeeDepartmentHistory>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.StartDate, e.DepartmentID, e.ShiftID });

                entity.HasIndex(e => e.DepartmentID).HasName("IX_EmployeeDepartmentHistory_DepartmentID");

                entity.HasIndex(e => e.ShiftID).HasName("IX_EmployeeDepartmentHistory_ShiftID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<EmployeePayHistory>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.RateChangeDate });

                entity.Property(e => e.RateChangeDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Rate).HasColumnType("money");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.ErrorTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Illustration>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<JobCandidate>(entity =>
            {
                entity.HasIndex(e => e.BusinessEntityID).HasName("IX_JobCandidate_BusinessEntityID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Availability)
                    .HasColumnType("decimal")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.CostRate)
                    .HasColumnType("smallmoney")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.PasswordHash).HasColumnType("varchar");

                entity.Property(e => e.PasswordSalt).HasColumnType("varchar");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_Person_rowguid").IsUnique();

                entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();

                entity.Property(e => e.EmailPromotion).HasDefaultValue(0);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.PersonType).HasColumnType("nchar");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<PersonCreditCard>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.CreditCardID });

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<PhoneNumberType>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductNumber).HasName("AK_Product_ProductNumber").IsUnique();

                entity.HasIndex(e => e.rowguid).HasName("AK_Product_rowguid").IsUnique();

                entity.Property(e => e.Class).HasColumnType("nchar");

                entity.Property(e => e.DiscontinuedDate).HasColumnType("datetime");

                entity.Property(e => e.ListPrice).HasColumnType("money");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ProductLine).HasColumnType("nchar");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.SellEndDate).HasColumnType("datetime");

                entity.Property(e => e.SellStartDate).HasColumnType("datetime");

                entity.Property(e => e.SizeUnitMeasureCode).HasColumnType("nchar");

                entity.Property(e => e.StandardCost).HasColumnType("money");

                entity.Property(e => e.Style).HasColumnType("nchar");

                entity.Property(e => e.Weight).HasColumnType("decimal");

                entity.Property(e => e.WeightUnitMeasureCode).HasColumnType("nchar");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_ProductCategory_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ProductCostHistory>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.StartDate });

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.StandardCost).HasColumnType("money");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_ProductDescription_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.LocationID });

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Quantity).HasDefaultValue(0);

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ProductListPriceHistory>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.StartDate });

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ListPrice).HasColumnType("money");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_ProductModel_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ProductModelIllustration>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelID, e.IllustrationID });

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<ProductModelProductDescriptionCulture>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelID, e.ProductDescriptionID, e.CultureID });

                entity.Property(e => e.CultureID).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<ProductPhoto>(entity =>
            {
                entity.Property(e => e.LargePhoto).HasColumnType("varbinary");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ThumbNailPhoto).HasColumnType("varbinary");
            });

            modelBuilder.Entity<ProductProductPhoto>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.ProductPhotoID });

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<ProductSubcategory>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_ProductSubcategory_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ProductVendor>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.BusinessEntityID });

                entity.HasIndex(e => e.BusinessEntityID).HasName("IX_ProductVendor_BusinessEntityID");

                entity.HasIndex(e => e.UnitMeasureCode).HasName("IX_ProductVendor_UnitMeasureCode");

                entity.Property(e => e.LastReceiptCost).HasColumnType("money");

                entity.Property(e => e.LastReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.StandardPrice).HasColumnType("money");

                entity.Property(e => e.UnitMeasureCode).HasColumnType("nchar");
            });

            modelBuilder.Entity<PurchaseOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.PurchaseOrderID, e.PurchaseOrderDetailID });

                entity.HasIndex(e => e.ProductID).HasName("IX_PurchaseOrderDetail_ProductID");

                entity.Property(e => e.PurchaseOrderDetailID).ValueGeneratedOnAdd();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LineTotal)
                    .HasColumnType("money")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ReceivedQty).HasColumnType("decimal");

                entity.Property(e => e.RejectedQty).HasColumnType("decimal");

                entity.Property(e => e.StockedQty)
                    .HasColumnType("decimal")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<PurchaseOrderHeader>(entity =>
            {
                entity.HasIndex(e => e.EmployeeID).HasName("IX_PurchaseOrderHeader_EmployeeID");

                entity.HasIndex(e => e.VendorID).HasName("IX_PurchaseOrderHeader_VendorID");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.RevisionNumber).HasDefaultValue(0);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.TaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderID, e.SalesOrderDetailID });

                entity.HasIndex(e => e.ProductID).HasName("IX_SalesOrderDetail_ProductID");

                entity.HasIndex(e => e.rowguid).HasName("AK_SalesOrderDetail_rowguid").IsUnique();

                entity.Property(e => e.SalesOrderDetailID).ValueGeneratedOnAdd();

                entity.Property(e => e.LineTotal)
                    .HasColumnType("numeric")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UnitPriceDiscount)
                    .HasColumnType("money")
                    .HasDefaultValue(0.0m);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.HasIndex(e => e.CustomerID).HasName("IX_SalesOrderHeader_CustomerID");

                entity.HasIndex(e => e.SalesOrderNumber).HasName("AK_SalesOrderHeader_SalesOrderNumber").IsUnique();

                entity.HasIndex(e => e.SalesPersonID).HasName("IX_SalesOrderHeader_SalesPersonID");

                entity.HasIndex(e => e.rowguid).HasName("AK_SalesOrderHeader_rowguid").IsUnique();

                entity.Property(e => e.CreditCardApprovalCode).HasColumnType("varchar");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.RevisionNumber).HasDefaultValue(0);

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.SalesOrderNumber).ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.TaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<SalesOrderHeaderSalesReason>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderID, e.SalesReasonID });

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<SalesPerson>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_SalesPerson_rowguid").IsUnique();

                entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();

                entity.Property(e => e.Bonus)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.CommissionPct)
                    .HasColumnType("smallmoney")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.SalesLastYear)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.SalesQuota).HasColumnType("money");

                entity.Property(e => e.SalesYTD)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);
            });

            modelBuilder.Entity<SalesPersonQuotaHistory>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.QuotaDate });

                entity.HasIndex(e => e.rowguid).HasName("AK_SalesPersonQuotaHistory_rowguid").IsUnique();

                entity.Property(e => e.QuotaDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.SalesQuota).HasColumnType("money");
            });

            modelBuilder.Entity<SalesReason>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<SalesTaxRate>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_SalesTaxRate_rowguid").IsUnique();

                entity.HasIndex(e => new { e.StateProvinceID, e.TaxType }).HasName("AK_SalesTaxRate_StateProvinceID_TaxType").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.TaxRate)
                    .HasColumnType("smallmoney")
                    .HasDefaultValue(0.00m);
            });

            modelBuilder.Entity<SalesTerritory>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_SalesTerritory_rowguid").IsUnique();

                entity.Property(e => e.CostLastYear)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.CostYTD)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.SalesLastYear)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.SalesYTD)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);
            });

            modelBuilder.Entity<SalesTerritoryHistory>(entity =>
            {
                entity.HasKey(e => new { e.BusinessEntityID, e.StartDate, e.TerritoryID });

                entity.HasIndex(e => e.rowguid).HasName("AK_SalesTerritoryHistory_rowguid").IsUnique();

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<ScrapReason>(entity =>
            {
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasIndex(e => new { e.StartTime, e.EndTime }).HasName("AK_Shift_StartTime_EndTime").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<ShipMethod>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_ShipMethod_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.ShipBase)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.ShipRate)
                    .HasColumnType("money")
                    .HasDefaultValue(0.00m);
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasIndex(e => new { e.ShoppingCartID, e.ProductID }).HasName("IX_ShoppingCartItem_ShoppingCartID_ProductID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Quantity).HasDefaultValue(1);
            });

            modelBuilder.Entity<SpecialOffer>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_SpecialOffer_rowguid").IsUnique();

                entity.Property(e => e.DiscountPct)
                    .HasColumnType("smallmoney")
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MinQty).HasDefaultValue(0);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SpecialOfferProduct>(entity =>
            {
                entity.HasKey(e => new { e.SpecialOfferID, e.ProductID });

                entity.HasIndex(e => e.ProductID).HasName("IX_SpecialOfferProduct_ProductID");

                entity.HasIndex(e => e.rowguid).HasName("AK_SpecialOfferProduct_rowguid").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.HasIndex(e => e.rowguid).HasName("AK_StateProvince_rowguid").IsUnique();

                entity.HasIndex(e => new { e.StateProvinceCode, e.CountryRegionCode }).HasName("AK_StateProvince_StateProvinceCode_CountryRegionCode").IsUnique();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

                entity.Property(e => e.StateProvinceCode).HasColumnType("nchar");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasIndex(e => e.SalesPersonID).HasName("IX_Store_SalesPersonID");

                entity.HasIndex(e => e.rowguid).HasName("AK_Store_rowguid").IsUnique();

                entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasIndex(e => e.ProductID).HasName("IX_TransactionHistory_ProductID");

                entity.HasIndex(e => new { e.ReferenceOrderID, e.ReferenceOrderLineID }).HasName("IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID");

                entity.Property(e => e.ActualCost).HasColumnType("money");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ReferenceOrderLineID).HasDefaultValue(0);

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.TransactionType).HasColumnType("nchar");
            });

            modelBuilder.Entity<TransactionHistoryArchive>(entity =>
            {
                entity.HasIndex(e => e.ProductID).HasName("IX_TransactionHistoryArchive_ProductID");

                entity.HasIndex(e => new { e.ReferenceOrderID, e.ReferenceOrderLineID }).HasName("IX_TransactionHistoryArchive_ReferenceOrderID_ReferenceOrderLineID");

                entity.Property(e => e.TransactionID).ValueGeneratedNever();

                entity.Property(e => e.ActualCost).HasColumnType("money");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ReferenceOrderLineID).HasDefaultValue(0);

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.TransactionType).HasColumnType("nchar");
            });

            modelBuilder.Entity<UnitMeasure>(entity =>
            {
                entity.Property(e => e.UnitMeasureCode).HasColumnType("nchar");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.HasIndex(e => e.ProductID).HasName("IX_WorkOrder_ProductID");

                entity.HasIndex(e => e.ScrapReasonID).HasName("IX_WorkOrder_ScrapReasonID");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StockedQty).ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<WorkOrderRouting>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderID, e.ProductID, e.OperationSequence });

                entity.HasIndex(e => e.ProductID).HasName("IX_WorkOrderRouting_ProductID");

                entity.Property(e => e.ActualCost).HasColumnType("money");

                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualResourceHrs).HasColumnType("decimal");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.PlannedCost).HasColumnType("money");

                entity.Property(e => e.ScheduledEndDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduledStartDate).HasColumnType("datetime");
            });
        }

        public virtual DbSet<AWBuildVersion> AWBuildVersion { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<BillOfMaterials> BillOfMaterials { get; set; }
        public virtual DbSet<BusinessEntity> BusinessEntity { get; set; }
        public virtual DbSet<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        public virtual DbSet<BusinessEntityContact> BusinessEntityContact { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<CountryRegion> CountryRegion { get; set; }
        public virtual DbSet<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        public virtual DbSet<CreditCard> CreditCard { get; set; }
        public virtual DbSet<Culture> Culture { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<CurrencyRate> CurrencyRate { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DatabaseLog> DatabaseLog { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EmailAddressTable> EmailAddress { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
        public virtual DbSet<EmployeePayHistory> EmployeePayHistory { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Illustration> Illustration { get; set; }
        public virtual DbSet<JobCandidate> JobCandidate { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Password> Password { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonCreditCard> PersonCreditCard { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductCostHistory> ProductCostHistory { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductInventory> ProductInventory { get; set; }
        public virtual DbSet<ProductListPriceHistory> ProductListPriceHistory { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductModelIllustration> ProductModelIllustration { get; set; }
        public virtual DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhoto { get; set; }
        public virtual DbSet<ProductProductPhoto> ProductProductPhoto { get; set; }
        public virtual DbSet<ProductReview> ProductReview { get; set; }
        public virtual DbSet<ProductSubcategory> ProductSubcategory { get; set; }
        public virtual DbSet<ProductVendor> ProductVendor { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        public virtual DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }
        public virtual DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
        public virtual DbSet<SalesPerson> SalesPerson { get; set; }
        public virtual DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistory { get; set; }
        public virtual DbSet<SalesReason> SalesReason { get; set; }
        public virtual DbSet<SalesTaxRate> SalesTaxRate { get; set; }
        public virtual DbSet<SalesTerritory> SalesTerritory { get; set; }
        public virtual DbSet<SalesTerritoryHistory> SalesTerritoryHistory { get; set; }
        public virtual DbSet<ScrapReason> ScrapReason { get; set; }
        public virtual DbSet<Shift> Shift { get; set; }
        public virtual DbSet<ShipMethod> ShipMethod { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public virtual DbSet<SpecialOffer> SpecialOffer { get; set; }
        public virtual DbSet<SpecialOfferProduct> SpecialOfferProduct { get; set; }
        public virtual DbSet<StateProvince> StateProvince { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistory { get; set; }
        public virtual DbSet<TransactionHistoryArchive> TransactionHistoryArchive { get; set; }
        public virtual DbSet<UnitMeasure> UnitMeasure { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<WorkOrder> WorkOrder { get; set; }
        public virtual DbSet<WorkOrderRouting> WorkOrderRouting { get; set; }

        // Unable to generate entity type for table 'Production.ProductDocument'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TableHavingColumnNamesWithSpaces'. Please see the warning messages.
        // Unable to generate entity type for table 'Production.Document'. Please see the warning messages.
        // Unable to generate entity type for table 'Person.PersonPhone'. Please see the warning messages.
    }
}