module SqlServerReverseEngineerTestE2E

open System
open Microsoft.SqlServer.Types
open Xunit
open FSharp.Data.Entity
open Microsoft.Data.Entity.Metadata
open Microsoft.Data.Entity

type DB = SqlServer<"Data Source=.;Initial Catalog=SqlServerReverseEngineerTestE2E;Integrated Security=True">
let db = new DB()
    
[<Fact>]
let AllDataTypes() = 
    let expected = [|
        typeof<int>, "AllDataTypesID"
        typeof<int64>, "bigintColumn"
        typeof<byte[]>, "binaryColumn"
        typeof<bool>, "bitColumn"
        typeof<string>, "charColumn"
        typeof<DateTime>, "dateColumn"
        typeof<DateTime Nullable>, "datetime24Column"
        typeof<DateTime Nullable>, "datetime2Column"
        typeof<DateTime Nullable>, "datetimeColumn"
        typeof<DateTimeOffset Nullable>, "datetimeoffset5Column"
        typeof<DateTimeOffset Nullable>, "datetimeoffsetColumn"
        typeof<decimal>, "decimalColumn"
        typeof<double>, "floatColumn"
        //typeof<SqlGeography>, "geographyColumn"
        //typeof<SqlGeometry>, "geometryColumn"
        //typeof<SqlHierarchyId Nullable>, "hierarchyidColumn"
        typeof<byte[]>, "imageColumn"
        typeof<int>, "intColumn"
        typeof<decimal>, "moneyColumn"
        typeof<string>, "ncharColumn"
        typeof<string>, "ntextColumn"
        typeof<decimal>, "numericColumn"
        typeof<string>, "nvarcharColumn"
        typeof<float32 Nullable>, "realColumn"
        typeof<DateTime Nullable>, "smalldatetimeColumn"
        typeof<int16>, "smallintColumn"
        typeof<decimal>, "smallmoneyColumn"
        //typeof<obj>, "sql_variantColumn"
        typeof<string>, "textColumn"
        typeof<TimeSpan Nullable>, "time4Column"
        typeof<TimeSpan Nullable>, "timeColumn"
        typeof<byte[]>, "timestampColumn"
        typeof<byte>, "tinyintColumn"
        typeof<Guid Nullable>, "uniqueidentifierColumn"
        typeof<byte[]>, "varbinaryColumn"
        typeof<string>, "varcharColumn"
        //typeof<string>, "xmlColumn"
    |]

    let actual =    
        typeof<DB.``dbo.AllDataTypes``>.GetProperties() 
        |> Array.map(fun p -> p.PropertyType, p.Name)
        |> Array.sortBy snd

    Assert.Equal(expected.Length, actual.Length)

    for x, y in Array.zip expected actual do
        Assert.Equal<_>(x, y, LanguagePrimitives.FastGenericEqualityComparer)

    let entityType = db.Model.FindEntityType( typeof<DB.``dbo.AllDataTypes``>)

    Assert.Equal(
        ValueGenerated.OnAddOrUpdate, 
        entityType.FindProperty("timestampColumn").ValueGenerated
    )

    Assert.Equal(Nullable 1, entityType.FindProperty("binaryColumn").GetMaxLength()    )
    Assert.Equal(Nullable 1, entityType.FindProperty("varbinaryColumn").GetMaxLength())

    Assert.Equal(Nullable 1, entityType.FindProperty("charColumn").GetMaxLength())
    Assert.Equal(Nullable 1, entityType.FindProperty("ncharColumn").GetMaxLength())
    Assert.Equal(Nullable 1, entityType.FindProperty("nvarcharColumn").GetMaxLength())
    Assert.Equal(Nullable 1, entityType.FindProperty("varcharColumn").GetMaxLength())

    Assert.Equal(Nullable 1, entityType.FindProperty("binaryColumn"). GetMaxLength())

    Assert.Equal<string>("binary", entityType.FindProperty("binaryColumn").SqlServer().ColumnType)

    Assert.Equal<string>("char", entityType.FindProperty("charColumn").SqlServer().ColumnType)
    Assert.Equal<string>("date", entityType.FindProperty("dateColumn").SqlServer().ColumnType)
    Assert.Equal<string>("image", entityType.FindProperty("imageColumn").SqlServer().ColumnType)
    Assert.Equal<string>("money", entityType.FindProperty("moneyColumn").SqlServer().ColumnType)
    Assert.Equal<string>("nchar", entityType.FindProperty("ncharColumn").SqlServer().ColumnType)
    Assert.Equal<string>("ntext", entityType.FindProperty("ntextColumn").SqlServer().ColumnType)
    Assert.Equal<string>("decimal", entityType.FindProperty("decimalColumn").SqlServer().ColumnType)
    Assert.Equal<string>("numeric", entityType.FindProperty("numericColumn").SqlServer().ColumnType)
    Assert.Equal<string>("smalldatetime", entityType.FindProperty("smalldatetimeColumn").SqlServer().ColumnType)
    Assert.Equal<string>("smallmoney", entityType.FindProperty("smallmoneyColumn").SqlServer().ColumnType)
    Assert.Equal<string>("text", entityType.FindProperty("textColumn").SqlServer().ColumnType)
    Assert.Equal<string>("timestamp", entityType.FindProperty("timestampColumn").SqlServer().ColumnType)
    Assert.Equal<string>("varbinary", entityType.FindProperty("varbinaryColumn").SqlServer().ColumnType)
    Assert.Equal<string>("varchar", entityType.FindProperty("varcharColumn").SqlServer().ColumnType)
//  types with explicit scale
    Assert.Equal<string>("datetime2(4)", entityType.FindProperty("datetime24Column").SqlServer().ColumnType)
    Assert.Equal<string>("datetime", entityType.FindProperty("datetimeColumn").SqlServer().ColumnType)
    Assert.Equal<string>("datetimeoffset(5)", entityType.FindProperty("datetimeoffset5Column").SqlServer().ColumnType)
    Assert.Equal<string>("time(4)", entityType.FindProperty("time4Column").SqlServer().ColumnType)

let getPrimaryKeyColumns(e: IEntityType) = 
    [ for p in e.FindPrimaryKey().Properties -> p.Name ]

let getSingleForeightKeyColumns(e: IEntityType) = 
    let fk = e.GetForeignKeys() |> Seq.exactlyOne
    [ for p in fk.Properties -> p.Name ]

[<Fact>]
let OneToManyDependent() = 
    let e = db.Model.FindEntityType( typeof<DB.``dbo.OneToManyDependent``>)
    Assert.Equal<_ list>(
        [ "OneToManyDependentID1"; "OneToManyDependentID2" ],
        getPrimaryKeyColumns(e)
    )
    Assert.Equal(Nullable 20, e.FindProperty("SomeDependentEndColumn").GetMaxLength())
    Assert.False(e.FindProperty("SomeDependentEndColumn").IsNullable)
    let nav = e.GetNavigations() |> Seq.exactlyOne
    Assert.Equal<_ list>(
        [ "OneToManyDependentFK1"; "OneToManyDependentFK2" ],
        [ for p in nav.ForeignKey.Properties -> p.Name ]
    )

    let inverseNav = 
        db.Model.FindEntityType( typeof<DB.``dbo.OneToManyPrincipal``>).GetNavigations() |> Seq.exactlyOne

    Assert.Same(nav, inverseNav.FindInverse())

[<Fact>]
let OneToManyPrincipal() = 
    let e = db.Model.FindEntityType( typeof<DB.``dbo.OneToManyPrincipal``>)
    Assert.Equal<_ list>(
        [ "OneToManyPrincipalID1"; "OneToManyPrincipalID2" ],
        getPrimaryKeyColumns(e)
    )

    let other = e.FindProperty("Other")
    Assert.Equal(Nullable 20, other.GetMaxLength())
    Assert.False( other.IsNullable)

[<Fact>]
let OneToOneFKToUniqueKeyDependent() = 
    let e = db.Model.FindEntityType( typeof<DB.``dbo.OneToOneFKToUniqueKeyDependent``>)
    Assert.Equal<_ list>(
        [ "OneToOneFKToUniqueKeyDependentID1"; "OneToOneFKToUniqueKeyDependentID2" ],
        getPrimaryKeyColumns(e)
    )    
    
    let ix = e.GetIndexes() |> Seq.exactlyOne
    Assert.Equal<_ []>(
        [| "OneToOneFKToUniqueKeyDependentFK1"; "OneToOneFKToUniqueKeyDependentFK2" |],
        [| for p in ix.Properties -> p.Name |]
    )
    Assert.True(ix.IsUnique)
    Assert.Equal<string>("UK_OneToOneFKToUniqueKeyDependent", ix.SqlServer().Name)   

[<Fact>]
let PropertyConfiguration() = 
    let e = db.Model.FindEntityType( typeof<DB.``dbo.PropertyConfiguration``>)
    let ix = e.GetIndexes() |> Seq.exactlyOne
    Assert.Equal<_ []>(
        [| "A"; "B" |],
        [| for p in ix.Properties -> p.Name |]
    )
    Assert.False(ix.IsUnique)
    Assert.Equal<string>("Test_PropertyConfiguration_Index", ix.SqlServer().Name)   
    
    let rowversionColumn = e.FindProperty("RowversionColumn")
    Assert.Equal<string>("timestamp", rowversionColumn.SqlServer().ColumnType)
    Assert.Equal(ValueGenerated.OnAddOrUpdate, rowversionColumn.ValueGenerated)

    Assert.Equal(ValueGenerated.OnAddOrUpdate, e.FindProperty("SumOfAAndB").ValueGenerated)

    Assert.Equal<string>("getdate()", e.FindProperty("WithDateDefaultExpression").SqlServer().GeneratedValueSql)
    Assert.Equal<string>("'October 20, 2015 11am'", e.FindProperty("WithDateFixedDefault").SqlServer().GeneratedValueSql)
    Assert.Equal(-1, e.FindProperty("WithDefaultValue").SqlServer().DefaultValue |> unbox)
    Assert.Equal(0.0M, e.FindProperty("WithMoneyDefaultValue").SqlServer().DefaultValue |> unbox)
    Assert.Equal<string>("newsequentialid()", e.FindProperty("WithGuidDefaultExpression").SqlServer().GeneratedValueSql)
    Assert.Null(e.FindProperty("WithVarcharNullDefaultValue").SqlServer().DefaultValue)
    Assert.Null(e.FindProperty("WithVarcharNullDefaultValue").SqlServer().GeneratedValueSql)
